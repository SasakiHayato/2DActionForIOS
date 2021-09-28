using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : EnemyBase
{
    AttackClass m_attack = new AttackClass();

    Rigidbody2D m_rb;
    Vector2 m_setVec = Vector2.zero;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        DefSpeed = Speed;
    }

    void Update()
    {
        Move();
    }

    public override void Attack()
    {
        
    }

    public override void Move()
    {
        FindPlayer();
        SetTrans(transform, Speed);
        m_rb.velocity = new Vector2(Speed, m_rb.velocity.y);
    }

    void FindPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (transform.position.x < player.position.x)
        {
            if (Speed < 0)
                Speed *= -1;
        }
        else if (transform.position.x > player.position.x)
        {
            if (Speed > 0)
                Speed *= -1;
        }
    }

    public override void Deid()
    {
        GameManager.Instance.Shake();
        Quaternion get = GameObject.FindGameObjectWithTag("Player").transform.localRotation;
        GameManager.Instance.InformDeid(gameObject, get, ThisID);
    }
}
