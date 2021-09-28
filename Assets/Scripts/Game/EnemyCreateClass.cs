using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateClass : MonoBehaviour
{
    [SerializeField] Transform[] m_spawnPos = new Transform[2];
    [SerializeField] float m_setTime;
    float m_time;

    void Update()
    {
        m_time += Time.deltaTime;

        if (m_time > m_setTime)
        {
            int set = Random.Range(0, m_spawnPos.Length);
            Vector2 setPos = m_spawnPos[set].position;
            GameManager.Instance.SetEnemys(setPos);

            m_time = 0;
        }
    }
}
