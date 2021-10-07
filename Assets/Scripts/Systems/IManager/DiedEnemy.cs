using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedEnemy : IManager
{
    List<Vector2> m_diedPosArray = new List<Vector2>();

    public void Execution()
    {
        Debug.Log("DiedEnemy");
        EnemyBase[] enemies = GameObject.FindObjectsOfType<EnemyBase>();
        foreach (EnemyBase enemy in enemies)
        {
            if (!enemy.IsDied) continue;
            m_diedPosArray.Add(enemy.transform.position);
        }

        foreach (var item in m_diedPosArray)
        {
            Debug.Log(item);
        }
    }
}
