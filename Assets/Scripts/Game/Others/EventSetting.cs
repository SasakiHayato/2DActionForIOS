using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSetting : MonoBehaviour
{
    public bool IsEnd { get; private set; }

    public void Set(int id, object set)
    {
        IsEnd = false;
        switch (id)
        {
            case 0:
                MoveTitlePlayer((GameObject)set);
                break;
        }
    }

    void MoveTitlePlayer(GameObject player)
    {
        Vector2 scale = player.transform.localScale;
        player.transform.localScale = new Vector2(scale.x * -1, scale.y);
        player.GetComponent<Animator>().Play("Player_Jump");
        player.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-5, 5) * 7, ForceMode2D.Impulse);

        Invoke("End", 0);
    }

    void End() => IsEnd = true;
}
