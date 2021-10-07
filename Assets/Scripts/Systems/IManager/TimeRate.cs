using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;
    public bool Do { get; set; }

    public void Execution()
    {
        if (!Do) return;
        Debug.Log("a");
    }
}
