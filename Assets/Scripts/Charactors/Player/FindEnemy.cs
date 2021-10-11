using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class FindEnemy
{
    List<IEnemys> m_target = new List<IEnemys>();
    Transform m_parent;

    public Player Player { private get; set; }

    public void Find(Transform parent)
    {
        m_parent = parent;
        EnemyBase[] enemies = GameObject.FindObjectsOfType<EnemyBase>();

        foreach (EnemyBase enemy in enemies)
        {
            IEnemys iEnemy = enemy.GetComponent<IEnemys>();
            if (iEnemy != null)
            {
                Check(iEnemy);
            }
        }
        if (m_target.Count == 0) return;
        if (m_target[0].GetPos() == null)
        {
            m_target[0] = m_target[1];
            m_target.Remove(m_target.Last());
        }

        for (int count = 0; count < m_target.Count; count++)
        {
            if (m_target[count] == null) break;
            Player.NearEnemy[count] = m_target[count].GetPos().position;
        }
        
        Sort();
    }

    void Check(IEnemys iEnemy)
    {
        if (m_target.Count == 0)
        {
            Add(iEnemy);
            return;
        }

        foreach (IEnemys enemy in m_target)
            if (iEnemy == enemy) return;

        Add(iEnemy);
    }

    void Add(IEnemys iEnemy)
    {
        if (m_target.Count < 2)
        {
            m_target.Add(iEnemy);
            return;
        }
        m_target.Add(iEnemy);
    }

    void Sort()
    {
        for (int i = 0; i < m_target.Count; i++)
        {
            for (int x = i + 1; x < m_target.Count; x++)
            {
                float dis1 = Vector2.Distance(m_parent.position, m_target[i].GetPos().position);
                float dis2 = Vector2.Distance(m_parent.position, m_target[x].GetPos().position);

                if (dis1 > dis2)
                {
                    IEnemys save = m_target[x];
                    m_target[x] = m_target[i];
                    m_target[i] = save;
                }
            }
        }

        if (m_target.Count <= 2)
            m_target.Remove(m_target.Last());
    }
}