using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemys;

[CreateAssetMenu (fileName = "EnemyDatas")]
public class EnemyData : ScriptableObject
{
    [SerializeField] List<Data> _datas = new List<Data>();
    public Data GetData(int id) => _datas[id];

    public int DataLength { get => _datas.Count; }
}

namespace Enemys
{
    [System.Serializable]
    public class Data
    {
        [SerializeField] string _name;
        [SerializeField] GameObject _obj;
        [SerializeField] int _hp;
        [SerializeField] float _speed;

        public string Name { get => _name; }
        public GameObject Obj { get => _obj; }
        public int Hp { get => _hp; }
        public float Speed { get => _speed; }
    }
}
