using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using BehaviorAI;

public class Enemy : EnemyBase, IBehavior, IEnemys, IDamageble
{
    [SerializeField] BehaviorTree _tree;
    CircleCollider2D _col;
    
    void Start()
    {
        _col = GetComponentInChildren<CircleCollider2D>();
        _col.enabled = false;
        //transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360)
        //    .SetLoops(-1).SetEase(Ease.Linear);
    }

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
                _col.enabled = false;
                if (RB.velocity.y <= 0) RB.drag = 100;
                break;
            case State.ImpactGround:
                transform.DOKill();
                RB.drag = 0;
                RB.mass = 10;
                break;
            case State.ImpactFloat:
                transform.DOKill();
                RB.drag = 0;
                RB.mass = 10;
                break;
        }
    }

    void SetCollider()
    {
        if (!_col.enabled) _col.enabled = true;
        else _col.enabled = false;
    }

    public void Call(IAction action) => action.Execute();
    public GameObject SetTarget() => gameObject;
    public GameObject GetObj() => gameObject;
    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("Enemy_Damage");
        Hp -= damage;
        if (Hp <= 0) Deid(gameObject);
    }
}
