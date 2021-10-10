using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera m_camera;
    List<GameObject> m_setList = new List<GameObject>();

    [SerializeField] Vector3 m_offset;

    [SerializeField] float m_min;
    [SerializeField] float m_max;
    [SerializeField] float m_limiter;
    [SerializeField] float m_cameraMove;

    Vector3 m_velo = Vector3.zero;

    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    void Update()
    {
        Zoom();
        Move();
    }

    void Zoom()
    {
        float zoom = Mathf.Lerp(m_max, m_min, GetDistance() / m_limiter);
        m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, zoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 pos = GetCenter() + m_offset;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref m_velo, m_cameraMove);
    }

    float GetDistance()
    {
        Bounds bounds = new Bounds();
        foreach (GameObject target in m_setList)
        {
            Vector2 set = target.transform.position;
            bounds.Encapsulate(set);
        }

        return bounds.size.x;
    }

    Vector3 GetCenter()
    {
        Bounds bounds = new Bounds(m_setList[0].transform.position, Vector3.zero);
        foreach (GameObject target in m_setList)
        {
            Vector2 set = target.transform.position;
            bounds.Encapsulate(set);
        }

        return bounds.center;
    }

    public void CheckListToAdd(GameObject get)
    {
        bool check = true;

        if (m_setList.Count <= 0)
        {
            m_setList.Add(get);
            return;
        }
        else
        {
            foreach (GameObject checkObject in m_setList)
            {
                if (get.name == checkObject.name)
                {
                    check = false;
                    break;
                }
            }
        }

        if (check)
            m_setList.Add(get);
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
                m_camera.CheckListToAdd(charactor);
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
