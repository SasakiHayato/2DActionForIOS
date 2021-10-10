using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiedEnemy : IManager
{
    [SerializeField] Sprite m_blood = null;
    [SerializeField] float m_spriteScale;

    int m_dirId = 0;

    public void Execution()
    {
        List<Vector2> diedPosArray = new List<Vector2>();
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyBase[] enemies = GameObject.FindObjectsOfType<EnemyBase>();

        foreach (EnemyBase enemy in enemies)
        {
            if (!enemy.IsDied) continue;
            diedPosArray.Add(enemy.transform.position);
        }

        foreach (Vector2 pos in diedPosArray)
        {
            GameObject blood = SetSprite();
            Quaternion q = CheckDir(playerPos.position, pos);
            Vector2 setPos = SetPosition(pos);

            blood.transform.position = setPos;
            blood.transform.localRotation = q;

            FadeClass fade = blood.AddComponent<FadeClass>();
            fade.SetFadeTarget<SpriteRenderer>(blood.GetComponent<SpriteRenderer>(), blood);
        }
    }

    GameObject SetSprite()
    {
        GameObject blood = new GameObject();
        blood.name = "Blood";
        blood.transform.localScale = new Vector2(m_spriteScale, m_spriteScale);
        SpriteRenderer sprite = blood.AddComponent<SpriteRenderer>();
        sprite.sprite = m_blood;

        return blood;
    }

    Vector2 SetPosition(Vector2 pos)
    {
        Vector2 setPos = Vector2.zero;
        if (m_dirId == 1)
            setPos = new Vector2(pos.x - m_spriteScale, pos.y + m_spriteScale);
        else if (m_dirId == 2)
            setPos = new Vector2(pos.x + m_spriteScale, pos.y + m_spriteScale);

        return setPos;
    }

    Quaternion CheckDir(Vector2 player, Vector2 target)
    {
        Quaternion q = Quaternion.identity;
        float dir = target.x - player.x;
        if (dir < 0)
        {
            q = Quaternion.Euler(0, 0, 0);
            m_dirId = 1;
        }
        else if (dir > 0)
        {
            q = Quaternion.Euler(0, 180, 0);
            m_dirId = 2;
        }

        return q;
    }
}
