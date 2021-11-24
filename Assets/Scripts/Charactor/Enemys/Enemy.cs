using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorAI;

public class Enemy : EnemyBase, IBehavior, IEnemys, IDamageble
{
    [SerializeField] BehaviorTree _tree;
    
    void Update()
    {
        base.FindPlayer(transform);
        switch (Current)
        {
            case State.IsGround:
                RB.mass = 1;
                RB.drag = 0;
                _tree.Repeater(this);
                break;
            case State.IsFloating:
                if (RB.velocity.y <= 0) RB.drag = 100;
                break;
            case State.ImpactGround:
                RB.drag = 0;
                RB.mass = 10;
                break;
            case State.ImpactFloat:
                RB.drag = 0;
                RB.mass = 10;
                break;
        }
    }

    public void Call(IAction action) => action.Execute();
    public GameObject SetTarget() => gameObject;
    public GameObject GetObj() => gameObject;
    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0) Deid(gameObject);
    }
}
