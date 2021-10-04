using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys, IDamageble
{
    public Vector3 GetPos() => transform.position;

    void Start()
    {

    }

    public float AddDamage()
    {
        return 1;
    }

    public void GetDamage(float damage)
    {
        Hp -= damage;

        if(Hp <= 0) Died();
    }
}
