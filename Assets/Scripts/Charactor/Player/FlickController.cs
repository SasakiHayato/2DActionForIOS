using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController
{
    Vector2 m_startPos = Vector2.zero;
    Vector2 m_endPos = Vector2.zero;

    float m_pushTime = 0;

    public int Dir { get; private set; }
    public bool IsSlide { get; private set; }

    public void IsPush()
    {
        if (Input.GetMouseButtonDown(0))
        {
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

        if (m_pushTime < 0.25f)
        {
            Debug.Log("フリック");
            IsSlide = false;
        }
        else
        {
            Debug.Log("スライド");
            IsSlide = true;
        }
    }

}
