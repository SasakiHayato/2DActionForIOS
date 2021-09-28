using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : EnemyBase
{
    [SerializeField] BulletClass m_bullet;
    Rigidbody2D m_rb;

    Vector2 m_setVec = Vector2.zero;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }


    public override void Attack()
    {
        
    }

    public override void Move()
    {
        
    }

    public override void Deid()
    {
        GameManager.Instance.Shake();
        Quaternion get = GameObject.FindGameObjectWithTag("Player").transform.localRotation;
        GameManager.Instance.InformDeid(gameObject, get, ThisID);
    }

    void SetBullet()
    {
        BulletClass bullet = Instantiate(m_bullet);
        Vector2 thisPos = transform.position;
        bullet.transform.position = thisPos + m_setVec * 2;
        bullet.SetDir(m_setVec, 20, gameObject);
    }
}
