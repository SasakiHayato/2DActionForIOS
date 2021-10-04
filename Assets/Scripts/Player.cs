using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float m_speed;

    Rigidbody2D m_rb;
    Flick m_flick = new Flick();

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
            m_flick.Pushed();

        else if (Input.GetMouseButton(0)) 
            m_flick.Pressing();

        if (Input.GetMouseButtonUp(0)) 
            m_flick.Separated();

        SetTrans();
        m_rb.velocity = new Vector2(m_flick.Dir * m_speed, m_rb.velocity.y);
    }

    void SetTrans()
    {
        Quaternion q = Quaternion.identity;

        if (m_flick.Dir == 1)
            q = Quaternion.Euler(0, 0, 0);
        else if (m_flick.Dir == -1)
            q = Quaternion.Euler(0, 180, 0);

        transform.localRotation = q;
    }
}

public class Flick
{
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
        m_pushTime = 0;
        Dir = 0;
    }

    private void SetDir()
    {
        float check = m_startPos.x - m_endPos.x;

        if (check > 1.5f) Dir = -1;
        else if (check < -1.5f) Dir = 1;
    }

    private void FilicOrSlede()
    {
        
    }
}
