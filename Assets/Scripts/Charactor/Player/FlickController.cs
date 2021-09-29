using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickController
{
    FlickCheckToMove m_flickCheck = new FlickCheckToMove();
    SlideCheckToAttack m_slideCheck = new SlideCheckToAttack();

    DrawLine m_line = new DrawLine();

    Vector2 m_startPos = Vector2.zero;
    Vector2 m_endPos = Vector2.zero;

    float m_pushTime = 0;
    float m_flickTime = 0.25f;
    int m_dir = 1;

    GameObject m_parent;

    public void IsPush(GameObject parent, PlayerController controller)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.EnemysSpeed(true);
            float front = m_dir * -1;
            Debug.Log(front);
            m_parent = parent;
            Vector3 mousePos = Input.mousePosition;
            m_startPos = Camera.main.ScreenToWorldPoint(mousePos);

            m_line.Drow(m_parent.transform, new Vector2(front, 0) * controller.AttackRange);
        }
    }

    public void Pressing(ref bool attack)
    {
        if (Input.GetMouseButton(0))
        {
            attack = true;
            m_pushTime += Time.deltaTime;
            
            Vector3 mousePos = Input.mousePosition;
            m_endPos = Camera.main.ScreenToWorldPoint(mousePos);

            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);
        }
    }

    public void Separated(ref bool attack)
    {
        if (Input.GetMouseButtonUp(0))
        {
            CheckDir();
            m_pushTime = 0;
            m_line.Des();
             attack = false;

            GameManager.Instance.EnemysSpeed(false);
            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);
        }
    }

    void CheckDir()
    {
        bool check = false;
        float dir = m_endPos.x - m_startPos.x;

        if (dir > 3)
        {
            Debug.Log("A");
            m_dir = 1;
            check = true;
        }
        else if (dir < -3)
        {
            Debug.Log("B");
            m_dir = -1;
            check = true;
        }
        if (m_pushTime < m_flickTime && check)
        {
            Debug.Log("フリック");
            m_flickCheck.IsFrick(m_parent, m_dir);
        }
        else if(m_pushTime >= m_flickTime && check)
        {
            Debug.Log("スライド");
            m_slideCheck.IsSlide(m_parent, m_dir);
        }
    }

}
