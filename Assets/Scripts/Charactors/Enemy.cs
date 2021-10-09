using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys, IDamageble
{
    AddForce m_force = new AddForce();

    Rigidbody2D m_rb;
    public Vector3 GetPos() => transform.position;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!base.GroundRay(transform)) return;

        FindPlayer();
        m_rb.velocity = new Vector2(Speed, m_rb.velocity.y);
    }

    void FindPlayer()
    {
        Quaternion q = Quaternion.identity;
        Transform playerPos = Player.transform;

        if (transform.position.x < playerPos.position.x)
        {
            q = Quaternion.Euler(0, 0, 0);
            if (Speed < 0)
                Speed *= -1;
        }
        else if (transform.position.x > playerPos.position.x)
        {
            q = Quaternion.Euler(0, 180, 0);
            if (Speed > 0)
                Speed *= -1;
        }

        transform.localRotation = q;
    }

    public float AddDamage()
    {
        return 1;
    }

    public void GetDamage(float damage)
    {
        Hp -= damage;
        m_force.Set(m_rb, transform, Player.transform);
        if(Hp <= 0) base.Died(gameObject);
    }
}
