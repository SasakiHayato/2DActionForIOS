using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

using Fields;
using SimpleFade;

public class EventSetting : MonoBehaviour
{
    public bool IsEnd { get; private set; }
    int _tutorialId = 1;

    Coroutine _tutorial;
    Coroutine _camera;

    public void Set(int id, object set = null)
    {
        IsEnd = false;
        switch (id)
        {
            case 0:
                MoveTitlePlayer();
                break;
            case 1:
                EntryPlayer();
                break;
            case 2:
                int bossID = (int)set;
                EntryBoss(bossID);
                break;
            case 3:
                SceneManage scene = (SceneManage)set;
                DeadPlayer(scene);
                break;
            case 4:
                StartCoroutine(SetResult());
                break;
            case 5:
                StartCoroutine(SkipTutorial());
                break;
        }
    }

    void MoveTitlePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 scale = player.transform.localScale;
        player.transform.localScale = new Vector2(scale.x * -1, scale.y);
        player.GetComponent<Animator>().Play("Player_Jump");
        player.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-3, 5) * 7, ForceMode2D.Impulse);

        Invoke(nameof(End), 0);
    }

    void EntryPlayer()
    {
        AudioManager.StopSource();
        GameObject player = Instantiate((GameObject)Resources.Load("TestPlayer"));
        
        player.transform.position = Vector2.one * 10;
        player.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-5, 0) * 100, ForceMode2D.Force);

        Fade.OutSingle(Fade.CreateFadeImage(), 1);
        _tutorial = StartCoroutine(Tutorial(player));

        CameraController camera = new CameraController();
        camera.SetUp();
        _camera = StartCoroutine(TutorialUpdate(camera));
    }

    IEnumerator Tutorial(GameObject player)
    {
        // 登場
        CheckGround ground = player.GetComponentInChildren<CheckGround>();
        player.GetComponent<Animator>().Play("TestPlayer_Fall");
        yield return new WaitUntil(() => ground.IsGround);

        // 着地
        AudioManager.LandSE();
        player.GetComponent<Animator>().Play("TestPlayer_Idle");
        yield return new WaitForSeconds(1f);

        // Enemy出現
        EnemyController enemy = new EnemyController();
        enemy.SetUp();
        enemy.Tutorial(_tutorialId);
        _tutorialId++;
        yield return new WaitForSeconds(1.5f);

        // スタートBGM. Player攻撃
        AudioManager.PlaySouce();
        GameObject.FindGameObjectWithTag("Enemy")
            .GetComponent<Animator>().Play("Enemy_Attack");
        UIManager.SetTUIData(1, player);
        player.GetComponent<Player>().TutorialEvent = true;
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // 諸々リセット
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        yield return new WaitForSeconds(1);

        // Enemy出現
        enemy.Tutorial(_tutorialId);
        yield return new WaitForSeconds(1.5f);
        
        // Player攻撃
        GameObject.FindGameObjectWithTag("Enemy")
            .GetComponent<Animator>().Play("Enemy_Attack");
        UIManager.SetTUIData(3, player);
        player.GetComponent<Player>().TutorialEvent = true;
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // 諸々リセット
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        _tutorialId++;
        yield return new WaitForSeconds(1);
        
        _tutorialId++;
        yield return null;

        // Player攻撃
        player.GetComponent<Player>().TutorialEvent = true;
        UIManager.SetTUIData(4, player);
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // 諸々リセット
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        yield return new WaitForSeconds(1);

        // Player攻撃
        player.GetComponent<Player>().TutorialEvent = true;
        UIManager.SetTUIData(5, player);
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // 諸々リセット
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        yield return new WaitForSeconds(1);

        Fade.InSingle(Fade.CreateFadeImage(), 1);
        yield return new WaitUntil(() => Fade.EndFade);
        yield return new WaitForSeconds(1.2f);
        GameManager.SetPlayerPos(player.transform.position);
        EndTutorial();
    }

    IEnumerator SkipTutorial()
    {
        StopCoroutine(_tutorial);
        StopCoroutine(_camera);
        AudioManager.StopSource();
        AudioManager.OnClickSE();
        Fade.InSingle(Fade.CreateFadeImage(), 1);
        yield return new WaitUntil(() => Fade.EndFade);
        yield return new WaitForSeconds(1.2f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameManager.SetPlayerPos(player.transform.position);
        EndTutorial();
    }

    void EndTutorial()
    {
        _tutorialId++;
        FindObjectOfType<SceneManage>().Load("Main");
    }

    IEnumerator TutorialUpdate(CameraController camera)
    {
        bool end = false;
        while (!end)
        {
            camera.Tutorial(_tutorialId);
            if (_tutorialId == 5) end = true;
            yield return null;
        }
    }

    void End() => IsEnd = true;

    void EntryBoss(int id)
    {
        // 気が向いたら
    }

    void DeadPlayer(SceneManage scene)
    {
        GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Animator>().Play("TestPlayer_Dead");
        StartCoroutine(Dead(scene));
    }

    IEnumerator Dead(SceneManage scene)
    {
        Fade.InSingle(Fade.CreateFadeImage(), 1);
        yield return new WaitUntil(() => Fade.EndFade);
        yield return new WaitForSeconds(1);
        scene.Load("Result");
    }

    IEnumerator SetResult()
    {
        Fade.OutSingle(Fade.CreateFadeImage(), 1);
        yield return new WaitUntil(() => Fade.EndFade);
        GameObject.Find("ResultPlayer").GetComponent<Animator>().Play("TestPlayer_Dead");
        RectTransform logRect = (RectTransform)GameObject.Find("LogText").transform;
        logRect.DOAnchorPosX(0, 0.3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.2f);
        RectTransform scoreRect = (RectTransform)GameObject.Find("Score").transform;
        scoreRect.DOAnchorPosX(0, 0.3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.2f);
        RectTransform phaseRect = (RectTransform)GameObject.Find("Phase").transform;
        phaseRect.DOAnchorPosX(0, 0.3f).SetEase(Ease.Linear);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        AudioManager.OnClickSE();
        Fade.InSingle(Fade.CreateFadeImage(), 1);
        yield return new WaitUntil(() => Fade.EndFade);
        FindObjectOfType<SceneManage>().Load("Title");
    }
}
