using UnityEngine;

public interface IEnemys
{
    GameObject GetObj();
    bool IsRockOn { get; set; }
}

public interface IDamageble
{
    float AddDamage();
    void GetDamage();
}
