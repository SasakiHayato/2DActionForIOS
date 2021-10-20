using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Œã‚©‚ç‚Å‚à‚æ‚µ
public class PlayerAI
{
    Player _player;
    GameObject _baseObj;
    Vector2 _enemyMinPos = Vector2.zero;

    public IEnemys NearEnemy { get; private set; }
    public void SetUp(GameObject set)
    {
        _baseObj = set;
        _player = set.GetComponent<Player>();
        NearEnemy = null;
    }
    public void UpDate()
    {
        SetNearEnemy();
        KeepDistance();
    }

    // NearEnemy‚Æ‚Ì‹——£‚Ì•ÛŠÇ
    void KeepDistance()
    {
        if (NearEnemy == null) return;
        float diff = Vector2.Distance(_baseObj.transform.position, NearEnemy.GetObj().transform.position);

        if (diff < 5) 
        {

            
        }
    }

    void SetNearEnemy()
    {
        List<IEnemys> iEnemys = GameManegement.Instnce().GetIEnemyList;
        if (iEnemys.Count == 0) return;

        foreach (IEnemys enemy in iEnemys)
        {
            if (_enemyMinPos == Vector2.zero)
            {
                _enemyMinPos = enemy.GetObj().transform.position;
                NearEnemy = enemy;
            }

            float minDis = Vector2.Distance(_player.transform.position, _enemyMinPos);
            float distance = Vector2.Distance
                (_player.transform.position, enemy.GetObj().transform.position);

            if (minDis > distance)
            {
                _enemyMinPos = enemy.GetObj().transform.position;
                NearEnemy = enemy;
            }
        }
    }
}
