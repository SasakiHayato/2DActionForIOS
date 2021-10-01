using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController
{
    FlickCheckToMove m_flickCheck = new FlickCheckToMove();
    
    DrawLine m_line = new DrawLine();

    Vector2 m_startPos = Vector2.zero;
    Vector2 m_endPos = Vector2.zero;

    float m_pushTime = 0;
    float m_flickTime = 0.25f;

    float m_dir = 1;
    public float Dir { get => m_dir; }

    GameObject m_parent;
    public bool IsSlide { get; set; }
    public bool IsPush { get; private set; }
    bool m_check = false;

    public void IsPress(GameObject parent)
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsSlide = false;

            GameManager.Instance.EnemysSpeed(true);
            m_parent = parent;
            Vector3 mousePos = Input.mousePosition;
            m_startPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    public void Pressing(ref bool attack, PlayerController controller)
    {
        if (Input.GetMouseButton(0))
        {
            attack = true;
            m_pushTime += Time.deltaTime;
            
            Vector3 mousePos = Input.mousePosition;
            m_endPos = Camera.main.ScreenToWorldPoint(mousePos);

            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);
            CheckDir();
            m_line.Drow(m_parent.transform, new Vector2(m_dir, 0) * controller.AttackRange);
        }
    }

    public void Separated(ref bool attack)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Set();

            m_pushTime = 0;
            m_line.Des();
             attack = false;

            GameManager.Instance.EnemysSpeed(false);
            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);  
        }
    }

    void CheckDir()
    {
        float dir = m_endPos.x - m_startPos.x;

        if (dir > 1.5f)
        {
            m_dir = 1;
            m_check = true;
        }
        else if (dir < -1.5f)
        {
            m_dir = -1;
            m_check = true;
        }
    }

    void Set()
    {
        if (m_pushTime < m_flickTime && m_check)
        {
            Debug.Log("フリック");
            m_flickCheck.IsFrick(m_parent, m_dir);
        }
        else if (m_pushTime >= m_flickTime && m_check)
        {
            Debug.Log("スライド");
            IsSlide = true;
        }

        m_check = false;
    }
}
