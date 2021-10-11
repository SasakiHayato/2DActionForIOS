using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickControl
{
    public Player GetPlayer { private get; set; }

    Vector2 m_startPos = Vector2.zero;
    Vector2 m_endPos = Vector2.zero;

    float m_pushTime;
    public float Dir { get; private set; }

    public void Pushed()
    {
        m_startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Pressing()
    {
        m_endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_pushTime += Time.deltaTime;

        SetDir();
    }

    public void Separated()
    {
        FlickCheck();
        Dir = 0;
        m_pushTime = 0;
    }

    private void SetDir()
    {
        float check = m_startPos.x - m_endPos.x;

        if (check > 1.5f) Dir = -1;
        else if (check < -1.5f) Dir = 1;
        else Dir = 0;
    }

    private void FlickCheck()
    {
        if (m_pushTime >= 0.2f || Dir == 0) return;
        GetPlayer.Attack();
        GameManager.Instance.GoSystem(IManage.Systems.TimeRate);
    }
}
