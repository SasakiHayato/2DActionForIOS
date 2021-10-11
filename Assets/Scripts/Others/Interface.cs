using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemys
{
    Transform GetPos();
}

public interface ICharactors
{
    GameObject GetObject();
}

public interface IDamageble
{
    float AddDamage();
    void GetDamage(float damage);
}

public interface IManager
{
    void Execution();
}

public interface IConditional
{
    bool Request();
    List<IEnemysBehaviour.ActionType> Answer();
}

public interface IBehaviourAction
{
    void Execution();
}
