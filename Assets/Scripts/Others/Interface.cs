using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemys
{
    Vector3 GetPos();
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
    void Answer();
}
