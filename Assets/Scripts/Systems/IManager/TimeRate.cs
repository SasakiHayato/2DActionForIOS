using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;
    public void Execution()
    {
        Debug.Log("TimeRate");
    }
}
