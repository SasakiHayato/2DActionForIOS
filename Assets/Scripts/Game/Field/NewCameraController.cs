using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fields
{
    public class NewCameraController
    {
        Player _player;
        GameObject _cameraObj;

        public void SetUp()
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            _player = obj.GetComponent<Player>();
        }
    }
}