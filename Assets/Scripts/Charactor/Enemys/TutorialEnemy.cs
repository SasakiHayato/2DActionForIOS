using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : EnemyBase, IEnemys, IDamageble
{
    void Start()
    {
        
    }

    void Update()
    {
        base.FindPlayer(transform);
        switch (Current)
        {
            case State.IsGround:
                RB.mass = 1;
                RB.drag = 0;
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

    public GameObject GetObj() => gameObject;
    public int AddDamage() => 1;
    public void GetDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0) Deid(gameObject);
    }
    public void Des() => Destroy(gameObject);
}
