using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPostion
{
    GameObject m_camera;

    public void GetCramera() => m_camera = GameObject.FindGameObjectWithTag("MainCamera");

    public void Set()
    {
        
    }
}
