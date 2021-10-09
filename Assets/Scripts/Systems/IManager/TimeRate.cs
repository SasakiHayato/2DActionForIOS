using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;
    [SerializeField] float m_resetTime;

    static bool m_isRunning = false;

    public void Execution()
    {
        if (m_isRunning) return;
        m_isRunning = true;

        GameObject set = new GameObject();
        set.name = "RateSyastem";

        RateControl rate = set.AddComponent<RateControl>();
        rate.Running = m_isRunning;
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

    public GameObject Target { private get; set; }
    public bool Running { private get; set; }

    public void Set(AnimationCurve curve, float reset)
    {
        StartCoroutine(SetTime(curve, reset));
    }

    IEnumerator SetTime(AnimationCurve curve, float reset)
    {
        float rate = float.MaxValue;
        while (rate > m_minValu)
        {
            m_rate = Mathf.Clamp(m_rate - m_rateSpeed, m_minValu, 1);
            Time.timeScale = curve.Evaluate(m_rate);
            
            rate = m_rate;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(ResetRate(reset));
    }

    IEnumerator ResetRate(float reset)
    {
        float time = 0;
        while (time < reset)
        {
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        Running = false;
        Time.timeScale = 1;
        Destroy(Target);
    }
}
