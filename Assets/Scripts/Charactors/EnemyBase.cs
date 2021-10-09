using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemyType;

public class EnemyBase : MonoBehaviour
{
    EnemyData m_data = new EnemyData();
    [SerializeField] Type m_type;

    Vector2 m_rayDir = new Vector2(0, -1);
    LayerMask m_groundMask; 

    float m_hp;
    float m_speed;

    public float Hp { get => m_hp; set { m_hp = value; } }
    public float Speed { get => m_speed; set { m_speed = value; } }
    public bool IsDied { get; private set; }

    GameObject m_player;
    public GameObject Player { get => m_player; set { m_player = value; } }

    private void Awake()
    {
        IsDied = false;
        m_data.Set(m_type, ref m_hp, ref m_speed);
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_groundMask = LayerMask.GetMask("Ground");
    }

    public virtual void Died(GameObject target)
    {
        IsDied = true;
        GameManager.Instance.GoSystem(IManage.Systems.DiedEnemy);
        Destroy(target);
    }

    public virtual bool GroundRay(Transform target)
    {
        RaycastHit2D hit = Physics2D.Raycast(target.position, m_rayDir, m_rayDir.magnitude * 1, m_groundMask);

        if (!hit.collider) return false;
        else return true;
    }
}

