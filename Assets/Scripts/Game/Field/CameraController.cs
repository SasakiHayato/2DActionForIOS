using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Fields
{
    class CameraController
    {
        GameObject _cameraObj;
        bool _shake = false;
        Vector3 _setShakeVec = Vector3.zero;
       
        public void SetUp()
        {
            _cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
            Camera cm = _cameraObj.GetComponent<Camera>();
            cm.orthographic = true;
            cm.orthographicSize = 10;
        }

        public void Mode()
        {
            if (_shake)
            {
                Shake();
                return;
            }

            if (FieldManagement.FieldCharas.Count <= 1) Normal();
            else if (FieldManagement.FieldCharas.Count > 1) Target();
        }

        void Normal()
        {
            Transform target = FieldManagement.FieldCharas.First().transform;
            Vector3 set = new Vector3(target.position.x, target.position.y, -10);
            _cameraObj.transform.position = set;
        }

        void Target()
        {

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

