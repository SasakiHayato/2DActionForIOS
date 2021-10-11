using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;
    [SerializeField] float m_resetTime;

    public bool IsRunning { get; set; } = false;

    public void Execution()
    {
        if (IsRunning) return;
        IsRunning = true;
        GameObject set = new GameObject();
        set.name = "RateSyastem";

        RateControl rate = set.AddComponent<RateControl>();
        rate.Rate = this;
        rate.Target = set;
        rate.Set(m_curve, m_resetTime);
    }
}

class RateControl : MonoBehaviour
{
    // Š„‡
    float m_rate = 1;

    float m_rateSpeed = 0.05f;
    float m_minValu = 0.15f;

    float m_waitTime = 0.05f;
    float m_resetTime;

    public GameObject Target { private get; set; }
    public TimeRate Rate { get; set; }

    public void Set(AnimationCurve curve, float reset)
    {
        m_resetTime = reset;
        StartCoroutine(SetTime(curve));
    }

    IEnumerator SetTime(AnimationCurve curve)
    {
        float rate = float.MaxValue;
        while (rate > m_minValu)
        {
            m_rate = Mathf.Clamp(m_rate - m_rateSpeed, m_minValu, 1);
            Time.timeScale = curve.Evaluate(m_rate);
            
            rate = m_rate;
            yield return new WaitForSeconds(m_waitTime);
        }

        StartCoroutine(ResetRate());
    }

    IEnumerator ResetRate()
    {
        float time = 0;
        while (time < m_resetTime)
        {
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        Rate.IsRunning = false;
        Destroy(Target);
    }
}
