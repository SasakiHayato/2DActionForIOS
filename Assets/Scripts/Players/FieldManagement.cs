using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManagement : MonoBehaviour
{
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Vector2 _setPos;

    private static FieldManagement _instance = new FieldManagement();
    public static FieldManagement Instance => _instance;

    public static List<IEnemys> IEnmysList { get; set; } = new List<IEnemys>(); 
    EnemyController _enemyctrl = new EnemyController();

    void Start()
    {
        _enemyctrl.SetUpEnemy = _enemyData;
    }

    public void CreateEnemys()
    {
        _enemyctrl.Create();
        _enemyctrl.SetUp(_setPos);
    }
}

class EnemyController
{
    public EnemyData SetUpEnemy { private get; set; } = null;
    
    GameObject _setEnemy = default;
    Enemys.Data _data = null;

    public void Create()
    {
        int set = Random.Range(0, SetUpEnemy.DataLength);
        _data = SetUpEnemy.GetData(set);
        GameObject obj = _data.Obj;
        IEnemys ienemy = obj.GetComponent<IEnemys>();
        if (ienemy != null) FieldManagement.IEnmysList.Add(ienemy);
        _setEnemy = obj;
    }

    public void SetUp(Vector2 pos)
    {
        if (_setEnemy == null || _data == null) return;

        GameObject get = MonoBehaviour.Instantiate(_setEnemy, pos, Quaternion.identity);
        EnemyBase enemy = get.GetComponent<EnemyBase>();
        enemy.Speed = _data.Speed;
        enemy.Hp = _data.Hp;
        
        _setEnemy = null;
        _data = null;
    }
}
