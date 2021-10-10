using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    List<Vector2> m_target = new List<Vector2>();
    List<GameObject> m_setList = new List<GameObject>();

    void Update()
    {
        
    }

    public void CheckList(GameObject get)
    {
        if (m_setList.Count <= 0)
        {
            m_setList.Add(get);
        }
        else
        {

        }
    }
}

class CameraManage : IManager
{
    CameraControl m_camera;
    bool m_call = false;

    public void Execution()
    {
        FirstCall();

        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject charactor in objects)
        {
            ICharactors iChara = charactor.GetComponent<ICharactors>();
            if (iChara != null)
            {
                m_camera.CheckList(charactor);
            }
        }
    }

    void FirstCall()
    {
        if (m_call) return;
        m_call = true;
        if (m_camera == null)
        {
            m_camera = GameObject.FindObjectOfType<CameraControl>();
        }
    }
}
