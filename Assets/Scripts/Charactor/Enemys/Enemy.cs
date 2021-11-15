using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys, IDamageble
{
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
            RB.gravityScale = 1;
            RB.velocity = new Vector2(Speed, RB.velocity.y);
        }
        else if (Current == State.IsFloating)
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
            if (RB.velocity.y <= 0) RB.gravityScale = 0;
        }
        else if (Current == State.Impact)
        {
            Debug.Log("Impact");
        }
    }

    public override void Attack()
    {
       
    }

    public override void AttackMove(int combo = 0)
    {
        Debug.Log(combo);
    }

    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {
        
    }

    public bool IsRockOn { get; set; }
    public GameObject GetObj() => gameObject;
}
