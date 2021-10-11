using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemyType;

public class EnemyBase : MonoBehaviour, ICharactors
{
    EnemyData m_data = new EnemyData();
    [SerializeField] Type m_type;

    float m_hp;
    float m_speed;
    float m_defSpeed;

    public float Hp { get => m_hp; set { m_hp = value; } }
    public float Speed { get => m_speed; set { m_speed = value; } }
    public bool IsDied { get; private set; } = false;
    public bool IsGround { get; private set; }

    GameObject m_player;
    public GameObject Player { get => m_player; set { m_player = value; } }

    private void Awake()
    {
        m_data.Set(m_type, ref m_hp, ref m_speed);
        m_defSpeed = Speed;

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        GameManager.Instance.GoSystem(IManage.Systems.CameraManage);
    }

    public virtual void Died(GameObject target)
    {
        IsDied = true;
        GameManager.Instance.GoSystem(IManage.Systems.DiedEnemy);
        Destroy(target);
    }

    public GameObject GetObject() => gameObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
            Speed = m_defSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            IsGround = false;
    }
}

