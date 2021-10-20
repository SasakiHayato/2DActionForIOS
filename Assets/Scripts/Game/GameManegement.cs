using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManegement : MonoBehaviour
{
    static GameManegement _instnce = new GameManegement();
    public static GameManegement Instnce() => _instnce;
    //private GameManegement() { }

    [SerializeField] EnemyData _data;
    [SerializeField] float _createTime;
    float _time;

    static CreateEnemys _enemys = new CreateEnemys();
    public List<IEnemys> GetIEnemyList { get => _enemys.EnemyLength; }
    private void Awake()
    {
        _enemys.SetUp(_data);
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_createTime < _time)
        {
            _enemys.Create();
            _time = 0;
        }
    }
}

class CreateEnemys
{
    EnemyData _datas;
    List<IEnemys> _enemysList = new List<IEnemys>();

    public List<IEnemys> EnemyLength { get => _enemysList; }
    public void SetUp(EnemyData data) => _datas = data;

    public void Create()
    {
        int set = Random.Range(0, _datas.DataLength);
        GameObject obj = MonoBehaviour.Instantiate(_datas.GetData(set).Obj);
        IEnemys enemy = obj.GetComponent<IEnemys>();
        
        if (enemy != null) _enemysList.Add(enemy);
    }
}
