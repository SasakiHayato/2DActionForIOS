using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleFade;

namespace Fields
{
    class EnemyController
    {
        public EnemyData SetUpEnemy { private get; set; } = null;
        
        GameObject _setEnemy = default;
        Enemys.Data _data = null;
        SpriteRenderer _sprite;

        int _setXval = 5;
        int _count = 0;

        ObjectPool<GameObject> _sLPool = new ObjectPool<GameObject>();

        public void SetUp()
        {
            _sLPool.Create((GameObject)Resources.Load("SpawnLight"));
        }

        public void Setting()
        {
            Create();
            SetData();
            Spawn();
        }

        public void Tutorial(int id)
        {
            GameObject obj = Object.Instantiate((GameObject)Resources.Load("TutorialTestEnemy"));
            obj.name = $"Enemy No.{_count}";
            _count++;
            _sprite = obj.GetComponent<SpriteRenderer>();
            _sprite.enabled = false;

            EnemyBase enemy = obj.GetComponent<EnemyBase>();
            enemy.Speed = 3;
            enemy.Hp = 2;
            FieldManagement.EnemysList.Add(obj.GetComponent<IEnemys>());
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            
            _sprite.enabled = true;

            float set = 0;
            if (id == 1) set = 5;
            else set = -3;
            Vector2 setPos = new Vector2(playerPos.x + set, -2);
            obj.transform.position = setPos;
            Light(setPos);
        }

        void Create()
        {
            int set = Random.Range(0, SetUpEnemy.DataLength);
            _data = SetUpEnemy.GetData(set);
            
            GameObject obj = MonoBehaviour.Instantiate(_data.Obj);
            obj.name = $"Enemy No.{_count}";
            _count++;
            _sprite = obj.GetComponent<SpriteRenderer>();
            _sprite.enabled = false;
            
            _setEnemy = obj;

            FieldManagement.FieldCharas.Add(obj);
        }

        void SetData()
        {
            IEnemys iEnemy = _setEnemy.GetComponent<IEnemys>();
            if (iEnemy != null) FieldManagement.EnemysList.Add(iEnemy);

            EnemyBase enemy = _setEnemy.GetComponent<EnemyBase>();
            enemy.Speed = _data.Speed;
            enemy.Hp = _data.Hp;

            _data = null;
        }

        void Spawn()
        {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            int[] setPosX = { _setXval * -1, _setXval };
            int randomX = Random.Range(0, setPosX.Length);
            
            Light(Enemy(new Vector2(playerPos.x + setPosX[randomX], -2)));
        }

        Vector2 Enemy(Vector2 setPos)
        {
            _sprite.enabled = true;
            return _setEnemy.transform.position = setPos;
        }

        void Light(Vector2 enemyPos)
        {
            GameObject light = _sLPool.Use();
            light.transform.position = enemyPos;
            light.GetComponent<DeleteUI>().SetAction(_sLPool.Delete);
            Fade.OutSingle(light.GetComponent<SpriteRenderer>(), 0.3f);
        }
    }
}
