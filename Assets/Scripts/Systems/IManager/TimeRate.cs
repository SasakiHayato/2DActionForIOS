using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

class TimeRate : IManager
{
    [SerializeField] AnimationCurve m_curve = null;
    public async void Execution()
    {
        EnemyBase enemyBase = GameObject.FindObjectOfType<EnemyBase>();

        await Task.Run(() => Rate());
        Debug.Log("End");
    }

    void Rate()
    {
        float time = 0;
        while (time >= 3)
        {
            time += Time.deltaTime;
            Debug.Log(time);
        }
    }
}
