using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys, IDamageble
{
    Rigidbody2D m_rb;
    public Vector3 GetPos() => transform.position;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        Quaternion q = Quaternion.identity;
        Transform playerPos = Player.transform;

        if (transform.position.x < playerPos.position.x)
            q = Quaternion.Euler(0, 0, 0);

        else if (transform.position.x > playerPos.position.x)
            q = Quaternion.Euler(0, 180, 0);

        transform.localRotation = q;
    }

    public float AddDamage()
    {
        return 1;
    }

    public void GetDamage(float damage)
    {
        Hp -= damage;

        if(Hp <= 0) Died(gameObject);
    }
}
