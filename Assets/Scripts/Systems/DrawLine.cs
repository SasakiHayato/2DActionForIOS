using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine
{
    LineRenderer m_line;

    Vector3 m_startPos = Vector3.zero;
    Vector3 m_endPos = Vector3.zero;
    

    public void Draw(Transform target, Vector3 dir, float range)
    {
        if (m_line == null)
            m_line = target.gameObject.AddComponent<LineRenderer>();

        Vector3[] set = new Vector3[]
        {
            m_startPos = new Vector3(target.position.x, target.position.y),
            m_endPos = new Vector3(dir.x * range + target.position.x, target.position.y),
        };

        m_line.startWidth = 0.1f;
        m_line.endWidth = 0.1f;

        m_line.SetPositions(set);
    }

    public void DeleteLine()
    {
        Vector3[] set = new Vector3[]
        {
            m_startPos = new Vector2(0, 0),
            m_endPos = new Vector2(0, 0),
        };

        m_line.SetPositions(set);
    }
}
