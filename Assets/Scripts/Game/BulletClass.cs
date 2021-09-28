using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttackType;
using GetEnumToGame;

public class BulletClass : MonoBehaviour
{
    GameObject m_parent;
    AttackClass m_attack = new AttackClass();
    Vector2 m_vec = Vector2.zero;

    public void SetDir(Vector2 setVec, float power, GameObject parent)
    {
        m_vec = setVec;
        m_parent = parent;
        Shot(setVec, power);
    }

    void Shot(Vector2 setVec, float power)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(setVec * power, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            m_attack.Set(m_vec * 20, m_parent, Parent.Enemy, Type.Bullet);

        Destroy(gameObject);
    }
}
