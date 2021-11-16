using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class NewEnemy : EnemyBase, IBehaviorTree, IEnemys, IDamageble
{
    [SerializeField] BehaviorTree _tree;
    
    void Update()
    {
        _tree.Repeater(this);
        base.FindPlayer(transform);
    }

    public void Set(IAction action) => action.Action();
    public override void AttackMove(int combo = 0)
    {
        Debug.Log("ss");
    }

    public GameObject GetObj() => gameObject;
    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0) Deid(gameObject);
    }
}
