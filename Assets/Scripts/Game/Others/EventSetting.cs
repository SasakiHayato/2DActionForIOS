using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Fields;
using SimpleFade;

public class EventSetting : MonoBehaviour
{
    public bool IsEnd { get; private set; }
    int _tutorialId = 1;

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

        Invoke("End", 0);
    }

    void EntryPlayer()
    {
        AudioManager.StopSource();
        GameObject player = Instantiate((GameObject)Resources.Load("TestPlayer"));
        
        player.transform.position = Vector2.one * 10;
        player.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-5, 0) * 100, ForceMode2D.Force);

        Fade.OutSingle(Fade.CreateFadeImage(), 1);
        StartCoroutine(Tutorial(player));

        CameraController camera = new CameraController();
        camera.SetUp();
        StartCoroutine(TutorialUpdate(camera));
    }

    IEnumerator Tutorial(GameObject player)
    {
        // �o��
        CheckGround ground = player.GetComponentInChildren<CheckGround>();
        player.GetComponent<Animator>().Play("TestPlayer_Fall");
        yield return new WaitUntil(() => ground.IsGround);

        // ���n
        AudioManager.LandSE();
        player.GetComponent<Animator>().Play("TestPlayer_Idle");
        yield return new WaitForSeconds(1f);

        // Enemy�o��
        EnemyController enemy = new EnemyController();
        enemy.Tutorial(_tutorialId);
        _tutorialId++;
        yield return new WaitForSeconds(1.5f);

        // �X�^�[�gBGM. Player�U��
        AudioManager.PlaySouce();
        GameObject.FindGameObjectWithTag("Enemy")
            .GetComponent<Animator>().Play("Enemy_Attack");
        UIManager.SetTUIData(1, player);
        player.GetComponent<Player>().TutorialEvent = true;
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // ���X���Z�b�g
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        yield return new WaitForSeconds(1);

        // Enemy�o��
        enemy.Tutorial(_tutorialId);
        yield return new WaitForSeconds(1.5f);
        
        // Player�U��
        GameObject.FindGameObjectWithTag("Enemy")
            .GetComponent<Animator>().Play("Enemy_Attack");
        UIManager.SetTUIData(3, player);
        player.GetComponent<Player>().TutorialEvent = true;
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // ���X���Z�b�g
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        _tutorialId++;
        yield return new WaitForSeconds(1);
        
        _tutorialId++;
        yield return null;

        // Player�U��
        player.GetComponent<Player>().TutorialEvent = true;
        UIManager.SetTUIData(4, player);
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // ���X���Z�b�g
        player.GetComponent<Player>().TutorialEvent = false;
        player.GetComponent<TutorialPlayer>().ResetBool();
        UIManager.SetTUIData(2);
        yield return new WaitForSeconds(1);

        // Player�U��
        player.GetComponent<Player>().TutorialEvent = true;
        UIManager.SetTUIData(5, player);
        yield return new WaitUntil(() => player.GetComponent<TutorialPlayer>().GetBool);

        // ���X���Z�b�g
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
        // �C����������
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
}
