using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class PlayerAI
    {
        public IEnemys NearEnemy { get; private set; } = null;
        public float Move { get => _speed; }

        float _speed = 0;
        float _setSpeed = 1.2f;
        float _setDis = 7;

        public void SetNearEnemy(Transform player)
        {
            if (FieldManagement.EnmysList.Count <= 0) return;

            float check = float.MaxValue;
            FieldManagement.EnmysList.ForEach(e =>
            {
                float distance = Vector2.Distance(player.position, e.GetObj().transform.position);
                if (check > distance)
                {
                    check = distance;
                    NearEnemy = e;
                }
            });
        }

        public void SupportMove(Transform player, bool isPress, bool isMove)
        {
            //if (NearEnemy == null || isPress || isMove)
            //{
            //    _speed = 0;
            //    return;
            //}
            
            //Vector2 enemyPos = NearEnemy.GetObj().transform.position;
            //float dis = Vector2.Distance(player.position, enemyPos);

            //if (dis < _setDis)
            //{
            //    if (player.localScale.x > 0) _speed = _setSpeed * -1;
            //    else _speed = _setSpeed;
            //}
            //else
            //{
            //    _speed = 0;
            //}
        }
    }
}