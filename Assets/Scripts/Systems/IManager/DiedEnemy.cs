using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiedEnemy : IManager
{
    [SerializeField] Sprite m_blood = null;
    List<Vector2> m_diedPosArray = new List<Vector2>();

    public void Execution()
    {
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyBase[] enemies = GameObject.FindObjectsOfType<EnemyBase>();

        foreach (EnemyBase enemy in enemies)
        {
            if (!enemy.IsDied) continue;
            m_diedPosArray.Add(enemy.transform.position);
        }

        foreach (Vector2 pos in m_diedPosArray)
        {
            SetSprite();
            CheckDir(playerPos.position, pos);
        }
    }

    GameObject SetSprite()
    {
        GameObject blood = new GameObject();
        blood.transform.localScale = new Vector2(2.5f, 2.5f);
        SpriteRenderer sprite = blood.AddComponent<SpriteRenderer>();
        sprite.sprite = m_blood;

        return blood;
    }

    Quaternion CheckDir(Vector2 player, Vector2 target)
    {
        Quaternion q = Quaternion.identity;
        float dir = target.x - player.x;
        if (dir < 0)
            q = Quaternion.Euler(0, 0, 0);
        else if (dir > 0)
            q = Quaternion.Euler(0, 180, 0);

        return q;
    }
}
