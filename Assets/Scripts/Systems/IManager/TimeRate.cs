using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;

    public void Execution()
    {
        EnemyBase[] enemyBase = GameObject.FindObjectsOfType<EnemyBase>();

        GameObject set = new GameObject();
        set.name = "RateSyastem";

        RateControl rate = set.AddComponent<RateControl>();
        rate.Target = set;
        rate.Set(m_curve, enemyBase);
    }
}

class RateControl : MonoBehaviour
{
    float m_rate = 0;
    float m_rateSpeed = 0.05f;

    public GameObject Target { private get; set; }

    public void Set(AnimationCurve curve, EnemyBase[] enemy)
    {
        StartCoroutine(SetTime(curve, enemy));
    }

    IEnumerator SetTime(AnimationCurve curve, EnemyBase[] enemy)
    {
        yield return new WaitForSeconds(2f);
        Destroy(Target);
    }
}
