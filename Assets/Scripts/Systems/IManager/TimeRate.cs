using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;

    public void Execution()
    {
        GameObject set = new GameObject();
        set.name = "RateSyastem";

        RateControl rate = set.AddComponent<RateControl>();
        rate.Target = set;
        rate.Set(m_curve);
    }
}

class RateControl : MonoBehaviour
{
    // Š„‡
    float m_rate = 1;

    float m_rateSpeed = 0.05f;
    float m_minValu = 0.15f;

    public GameObject Target { private get; set; }

    public void Set(AnimationCurve curve)
    {
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
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(Target);
    }
}
