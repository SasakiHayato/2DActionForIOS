using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharactorBase, IDamageble
{
    [SerializeField] float m_attackRange;

    FlickController m_flick = new FlickController();
    AttackClass m_attack = new AttackClass();

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

        if (m_attack.GetAttack)
        {
            m_attack.GetAttack = false;
            AroundAttack();
        }
    }

    void SetTrans(float dir)
    {
        if (dir < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else if (dir > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Attack()
    {
        Vector2 set = new Vector2(m_flick.Dir, 0) * m_attackRange;
        m_attack.Set(set, gameObject, GetEnumToGame.Parent.Player);
    }

    void AroundAttack()
    {
        Debug.Log("UŒ‚");
    }

    public float AddDamage() => Add;
    public void GetDamage(float damage)
    {
        Hp -= damage;
        GameManager.Instance.SetUiParam(UiType.Ui.PlayerHp);
        if (Hp <= 0)
        {
            GameManager.Instance.Shake();
            GameManager.Instance.DeidPlayer();
        }
    }
}
