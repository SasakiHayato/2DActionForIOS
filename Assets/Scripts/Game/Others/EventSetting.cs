using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        CheckGround ground = player.GetComponentInChildren<CheckGround>();
        yield return new WaitUntil(() => ground.IsGround);
        AudioManager.LandSE();
        yield return new WaitForSeconds(1f);
        EnemyController enemy = new EnemyController();
        enemy.Tutorial();
        _tutorialId++;
        yield return new WaitForSeconds(1.5f);
        AudioManager.PlaySouce();
        GameObject.FindGameObjectWithTag("Enemy")
            .GetComponent<Animator>().Play("Enemy_Attack");

        UIManager.SetTUIData(1, player);
    }

    IEnumerator TutorialUpdate(CameraController camera)
    {
        bool end = false;
        while (!end)
        {
            camera.Tutorial(_tutorialId);
            yield return null;
        }
    }

    void End() => IsEnd = true;
}
