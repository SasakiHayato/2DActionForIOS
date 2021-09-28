using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPostion
{
    Transform m_camera;

    public void GetCramera() => m_camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

    public void Set()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Quaternion q = player.localRotation;

        if (q.y == 0)
            m_camera.position = new Vector3(player.position.x + 1, player.position.y, m_camera.transform.position.z);
        else
            m_camera.position = new Vector3(player.position.x - 1, player.position.y, m_camera.transform.position.z);
    }
}
