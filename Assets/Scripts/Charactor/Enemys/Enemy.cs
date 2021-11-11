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
        if (!IsMove) return;
        
        Move();
    }

    public override void Move()
    {
        if (Current == State.IsGround)
        {
            _rb.gravityScale = 1;
            _rb.velocity = new Vector2(Speed, _rb.velocity.y);
        }
        else if (Current == State.IsFloating)
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector2.zero;
        }
    }

    public override void Attack()
    {
       
    }

    public override void AttackMove(AttackSetting.ActionType type, int combo = 0)
    {
        Debug.Log(combo);
    }

    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {
        Debug.Log($"�_���[�W : {damage} {gameObject.name}");
    }

    public bool IsRockOn { get; set; }
    public GameObject GetObj() => gameObject;
}