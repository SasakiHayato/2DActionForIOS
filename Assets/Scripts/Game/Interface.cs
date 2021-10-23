using UnityEngine;

public interface IEnemys
{
    GameObject GetObj();
    bool IsRockOn { get; set; }
}

public interface IDamageble
{
    int AddDamage();
    void GetDamage(float damage);
}

public interface IState
{
    State Current { get; }
    void ChangeState();
}