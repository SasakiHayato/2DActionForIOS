using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemyAI;

public class NewEnemy : MonoBehaviour, IEnemyAI
{
    [SerializeField] BehaviorTree _tree;
    
    void Update()
    {
        _tree.Repeater(this);
    }

    public void Set(IAction action) => action.Action();
}
