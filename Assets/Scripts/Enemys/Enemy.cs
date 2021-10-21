using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys, IDamageble, IState
{
    Rigidbody2D _rb;
    public State Current { get; private set; } = State.IsGround;
    
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

    public void Attack(State my, State other)
    {

    }

    public void ChangeState()
    {

    }
    
    public bool IsRockOn { get; set; }
    public GameObject GetObj() => gameObject;
}
