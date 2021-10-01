using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharactorBase, IDamageble
{
    [SerializeField] float m_attackRange;

    FlickController m_flick = new FlickController();
    SlideCheckToAttack m_slide = new SlideCheckToAttack();

    bool m_attackCheck;
    public bool CurrentAttack { get => m_attackCheck;}
    public float AttackRange { get => m_attackRange; private set { m_attackRange = value; } }

    void Start()
    {
        m_attackCheck = false;
    }

    void Update()
    {
        m_flick.IsPress(gameObject);
        m_flick.Pressing(ref m_attackCheck, this);
        m_flick.Separated(ref m_attackCheck);

        if (m_flick.IsSlide)
        {
            m_flick.IsSlide = false;
            Attack();
        }

        SetTrans(m_flick.Dir);
    }

    void SetTrans(float dir)
    {
        if (dir < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else if (dir > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Attack() => m_slide.IsSlide(gameObject, m_flick.Dir);

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
