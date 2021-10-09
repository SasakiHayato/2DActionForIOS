using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : IManager
{
    static CameraManage m_manage = new CameraManage();

    static Camera m_camera;
    static Player m_player;
    List<Vector2> m_targets = new List<Vector2>();

    public void Execution()
    {
        Call();
        PosList();
    }

    void PosList()
    {
        foreach (Vector2 target in m_targets)
        {

        }
    }

    void Call()
    {
        if (m_camera == null)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            m_camera = camera.GetComponent<Camera>();
        }

        if (m_player == null)
        {
            m_player = GameObject.FindObjectOfType<Player>();
        }
    }
}

class CameraManage
{

}
