using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Fields
{
    class CameraController
    {
        GameObject _cameraObj;
        Camera _cm;

        GameObject _playerObj;
        Player _player;

        bool _shake = false;
        Vector3 _setShakeVec = Vector3.zero;


       
        public void SetUp()
        {
            _playerObj = GameObject.FindGameObjectWithTag("Player");
            _player = _playerObj.GetComponent<Player>();
            _cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
            _cm = _cameraObj.GetComponent<Camera>();
            _cm.orthographic = true;
        }

        public void Mode()
        {
            if (_shake)
            {
                Shake();
                return;
            }

            if (FieldManagement.FieldCharas.Count <= 1) Normal();
            else Target();
        }

        void Normal()
        {
            Vector3 target = _playerObj.transform.position;
            Vector3 set = new Vector3(target.x, target.y, -10);
            _cameraObj.transform.position = set;
            _cm.orthographicSize = 10;
        }

        void Target()
        {
            if (_player.RequestIEnemy() == null) return;

            Vector3 playerPos = _playerObj.transform.position;
            Vector3 targetPos = _player.RequestIEnemy().GetObj().transform.position;
            Vector2 centar = (playerPos + targetPos) / 2;
            Zoom(Vector3.Distance(playerPos, targetPos));
            Vector3 set = new Vector3(centar.x, centar.y, -10);
            _cameraObj.transform.position = set;
        }

        void Zoom(float distance)
        {
            float z = Mathf.Lerp(10, 20, distance / 10);
            _cm.orthographicSize = z;
        }

        public void Shake()
        {
            if (!_shake)
            {
                _shake = true;
                _setShakeVec = _cameraObj.transform.position;
            }
            int x = Random.Range(-1, 1);
            int y = Random.Range(-1, 1);
            Vector3 set = new Vector3(_setShakeVec.x + x, _setShakeVec.y + y, _setShakeVec.z);
            _cameraObj.transform.position = set;
        }

        public void EndShake() => _shake = false;
    }

}

