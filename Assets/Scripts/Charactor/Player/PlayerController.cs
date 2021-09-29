using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GetEnumToGame;

public class PlayerController : CharactorBase, IDamageble
{
    [SerializeField] float m_attackRange;

    FlickController m_flick = new FlickController();

    bool m_attackCheck;
    public bool CurrentAttack { get => m_attackCheck;}
    public float AttackRange { get => m_attackRange; private set { m_attackRange = value; } }

    void Start()
    {
        m_attackCheck = false;
    }

    void Update()
    {
        m_flick.IsPush(gameObject, this);
        m_flick.Pressing(ref m_attackCheck);
        m_flick.Separated(ref m_attackCheck);
    }

    public float AddDamage() => Add;
    public void GetDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            GameManager.Instance.Shake();
            GameManager.Instance.DeidPlayer();
        }
    }
}
