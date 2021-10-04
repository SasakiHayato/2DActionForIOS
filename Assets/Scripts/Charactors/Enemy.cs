using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase, IEnemys
{
    public Vector3 GetPos() => transform.position;
}
