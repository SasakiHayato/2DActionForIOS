using UnityEngine;

public interface IEnemys
{
    GameObject GetObj();
}

public interface IDamageble
{
    int AddDamage();
    void GetDamage(int damage);
}

public interface IState
{
    State Current { get; }
    State ChangeState(State get);
}