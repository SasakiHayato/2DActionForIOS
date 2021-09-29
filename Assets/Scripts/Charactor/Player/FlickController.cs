using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController
{
    FlickCheckToMove m_flickCheck = new FlickCheckToMove();

    Vector2 m_startPos = Vector2.zero;
    Vector2 m_endPos = Vector2.zero;

    float m_pushTime = 0;
    float m_flickTime = 0.25f;

    public int Dir { get; private set; }
    public bool IsSlide { get; private set; }

    GameObject m_parent;

    public void IsPush(GameObject parent)
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_parent = parent;
            Vector3 mousePos = Input.mousePosition;
            m_startPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    public void Pressing()
    {
        if (Input.GetMouseButton(0))
        {
            m_pushTime += Time.deltaTime;
            
            Vector3 mousePos = Input.mousePosition;
            m_endPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    public void Separated()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CheckDir();
            m_pushTime = 0;
        }
    }

    void CheckDir()
    {
        if (m_startPos.x < m_endPos.x)
            Dir = 1;
        else if (m_startPos.x > m_endPos.x)
            Dir = -1;

        if (m_pushTime < m_flickTime)
        {
            Debug.Log("フリック");
            m_flickCheck.IsFrick(m_parent, Dir);
            IsSlide = false;
        }
        else
        {
            Debug.Log("スライド");
            IsSlide = true;
        }
    }

}
