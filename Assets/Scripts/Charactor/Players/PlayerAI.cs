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
    }
}