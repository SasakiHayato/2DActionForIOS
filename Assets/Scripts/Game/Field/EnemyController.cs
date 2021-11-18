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

        public void SetUp()
        {
            Create();
            SetData();
            Spawn();
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
            GameObject spawnPrefab = (GameObject)Resources.Load("SpawnLight");
            GameObject light = MonoBehaviour.Instantiate(spawnPrefab);
            light.transform.position = enemyPos;
            Fade.OutSingle(light.GetComponent<SpriteRenderer>(), 0.3f);
        }
    }
}
