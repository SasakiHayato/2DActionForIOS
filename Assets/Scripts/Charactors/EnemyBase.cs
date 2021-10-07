using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemyType;

public class EnemyBase : MonoBehaviour
{
    EnemyData m_data = new EnemyData();
    [SerializeField] Type m_type;

    Vector2 m_rayDir = new Vector2(0, -2);
    LayerMask m_groundMask; 

    float m_hp;
    float m_speed;
    float m_defSpeed;

    public float Hp { get => m_hp; set { m_hp = value; } }
    public float Speed { get => m_speed; private set { m_speed = value; } }

    GameObject m_player;
    public GameObject Player { get => m_player; set { m_player = value; } }

    private void Awake()
    {
        m_data.Set(m_type, ref m_hp, ref m_speed);
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_groundMask = LayerMask.GetMask("Ground");
        m_defSpeed = m_speed;
    }

    public void Died(GameObject target)
    {
        Destroy(target);
        Debug.Log("EnemyŽ€‚ñ‚¾");
    }

    public void GroundRay(Transform target)
    {
        RaycastHit2D hit = Physics2D.Raycast(target.position, m_rayDir, m_rayDir.magnitude * 2, m_groundMask);

        if (!hit.collider)
            Speed = 0;
        else
            Speed = m_defSpeed;
    }
}

