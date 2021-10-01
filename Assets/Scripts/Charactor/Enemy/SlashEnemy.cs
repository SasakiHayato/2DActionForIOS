using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnemy : EnemyBase
{
    [SerializeField] float m_attackRange;
    AttackClass m_attack = new AttackClass();
    DrawLine m_line = new DrawLine();

    Rigidbody2D m_rb;
    Vector2 m_setVec = Vector2.zero;

    bool m_attackCheck = false;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        DefSpeed = Speed;
    }

    void Update()
    {
        Move();
        Ray();
    }

    public override void Attack()
    {
        if (!m_attackCheck)
        {
            m_attackCheck = true;
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(3f);
        m_attackCheck = false;
        m_attack.Set(m_setVec, gameObject, GetEnumToGame.Parent.Enemy);
    }


    public override void Move()
    {
        FindPlayer();
        SetTrans(transform, Speed);
        m_rb.velocity = new Vector2(Speed, m_rb.velocity.y);
    }

    void Ray()
    {
        Vector2 dir = Vector2.zero;
        if (0 > Speed)
            dir = Vector2.left;
        else if (0 < Speed)
            dir = Vector2.right;

        m_line.Drow(transform, dir * m_attackRange);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.magnitude * m_attackRange, LayerMask.GetMask("Player"));

        if (hit.collider)
        {
            m_setVec = dir * m_attackRange;
            Attack();
        }
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
