using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys, IDamageble
{
    Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        base.FindPlayer(transform);
        Move();
    }

    public override void Move()
    {
        _rb.velocity = new Vector2(Speed, _rb.velocity.y);
    }

    public int AddDamage() => 1;
    public void GetDamage(float damage)
    {

    }

    public override void Attack(State other)
    {

    }
    
    public bool IsRockOn { get; set; }
    public GameObject GetObj() => gameObject;
}
