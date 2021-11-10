using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Fields
{
    class CameraController
    {
        Camera _mainCamera;
        Camera _shakeCm;
        CinemachineBrain _brain;
        public CinemachineTargetGroup TargetGroup { get; private set; }

        GameObject _player;
        GameObject _cameraObj;

        bool _isShake = false;
        string _shakeCmName = "ShekeCamera";

        public void SetUp()
        {
            _cameraObj = GameObject.FindGameObjectWithTag("MainCamera");

            TargetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
            _player = GameObject.FindGameObjectWithTag("Player");
            TargetGroup.AddMember(_player.transform, 1, 0);

            _brain = _cameraObj.GetComponent<CinemachineBrain>();
            _mainCamera = _cameraObj.GetComponent<Camera>();
        }

        public void Mode()
        {
            if (_isShake) return;

            if (TargetGroup.m_Targets.Length <= 1)
            {
                _brain.enabled = false;
                Noarmal();
            }
            else
            {
                _brain.enabled = true;
                Target();
            }
        }

        public Vector3 SetUpShake()
        {
            GameObject shakeCm = GameObject.Find(_shakeCmName);
            if (shakeCm == null) CreateShakeCamera();

            _shakeCm.transform.position = _cameraObj.transform.position;
            _shakeCm.orthographicSize = _mainCamera.orthographicSize;

            _isShake = true;
            _mainCamera.enabled = false;
            _brain.enabled = false;

            _shakeCm.enabled = true;

            return _cameraObj.transform.position;
        }

        public void IsShake(Vector3 set)
        {
            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);

            Vector3 setVec = new Vector3(set.x + x, set.y + y, set.z);
            _shakeCm.transform.position = setVec;
        }

        public void EndShake()
        {
            _isShake = false;
            _mainCamera.enabled = true;
            _brain.enabled = true;
            _shakeCm.enabled = false;
        }

        void CreateShakeCamera()
        {
            GameObject shakeCm = new GameObject(_shakeCmName);
            _shakeCm = shakeCm.AddComponent<Camera>();

            _shakeCm.backgroundColor = _mainCamera.backgroundColor;
            _shakeCm.orthographic = true;
        }

        void Noarmal()
        {
            _mainCamera.orthographicSize = 10;
            Vector2 playerPos = _player.transform.position;
            Vector2 playerScale = _player.transform.localScale;

            _cameraObj.transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        }

        void Target()
        {

        }
    }

}

