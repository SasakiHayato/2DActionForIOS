using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Players
{
    public class Controller
    {
        public IEnemys NearEnemy { get; set; } = null;

        bool _isPress = false;

        Vector2 _setUpPos = Vector2.zero;
        Vector2 _currentPos = Vector2.zero;

        float _time;

        public float FlickTime { private get; set; } = 0;
        public float FlickLimit { private get; set; } = 0;
        public bool IsMove { get; set; } = false;

        public Vector2 ForceVec { get;private set; } = Vector2.zero;
        public Player Player { private get; set; } = null;

        public void Update()
        {
            Pressed();
            Pressing();
            Released();
        }

        void Pressed()
        {
            if (IsMove) return;
            if (Input.GetMouseButtonDown(0))
            {
                _isPress = true;
                _setUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        void Pressing()
        {
            if (!_isPress) return;

            _currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _time += Time.deltaTime;
        }

        void Released()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_time < FlickTime)
                {
                    if (Vector2.Distance(_setUpPos, _currentPos) >= Mathf.Abs(FlickLimit))
                    {
                        Vector2 diffVec = _currentPos - _setUpPos;
                        float rad = Mathf.Atan2(diffVec.y, diffVec.x);
                        ForceVec = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                    }
                    else
                        ForceVec = Vector2.zero;
                   
                    Player.Attack();
                }

                _isPress = false;
                _time = 0;
            }
        }

        public void SetNearEnemy(Transform player)
        {

            if (FieldManagement.EnemysList.Count <= 0)
            {
                NearEnemy = null;
                return;
            }

            float check = float.MaxValue;
            FieldManagement.EnemysList.ForEach(e =>
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
