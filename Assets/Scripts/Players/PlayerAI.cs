using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class PlayerAI
    {
        public IEnemys NearEnemy { get; private set; } = null;
        public float Move
        {
            get
            {
                if (_isSupport) return 1;
                else return 0;
            }
        }

        bool _isSupport = false;

        public void SetNiarEnemy(Transform player)
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

        public void SupportMove()
        {
            _isSupport = true;
        }
    }
}