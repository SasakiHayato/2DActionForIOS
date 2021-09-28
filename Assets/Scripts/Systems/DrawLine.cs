using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine
{
    LineRenderer m_line;

    public void Drow(Transform parent, Vector3 endVec)
    {
        m_line = GameObject.FindObjectOfType<LineRenderer>();

        m_line.startWidth = 0.05f;
        m_line.endWidth = 0.05f;

        Vector3[] pos = new Vector3[]
        {
            new Vector3(parent.position.x, parent.position.y, parent.position.z),
            new Vector3(parent.position.x + endVec.x,parent.position.y + endVec.y, 0),
        };

        m_line.SetPositions(pos);
    }

    public void Des()
    {
        Vector3[] pos = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
        };

        m_line.SetPositions(pos);
    }
}
