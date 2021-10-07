using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeClass : MonoBehaviour
{
    bool m_isFade = false;
    object m_target = null;

    void Update()
    {
        if (!m_isFade) return;
        Debug.Log("UpDate");
    }

    public void SetFadeTarget<T>(T target, bool set)
    {
        m_isFade = set;
        m_target = target;
        
    }
}
