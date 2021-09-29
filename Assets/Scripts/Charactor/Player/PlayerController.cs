using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GetEnumToGame;

public class PlayerController : CharactorBase, IDamageble
{
    [SerializeField] float m_attackRange;
    Rigidbody2D m_rb;

    AttackClass m_attackClass = new AttackClass();
    DrawLine m_line = new DrawLine();
    FlickController m_flick = new FlickController();

    float m_rate = 1;
    public bool CurrentAttack { get; private set; }

    void Start()
    {
        CurrentAttack = false;
    }

    void Update()
    {
        float front = 0;
        float h = Move(ref front);
        Attack(ref front);

        m_flick.IsPush(gameObject);
        m_flick.Pressing();
        m_flick.Separated();
    }

    float Move(ref float front)
    {
        float h = Input.GetAxisRaw("Horizontal") * Speed;

        if (m_flick.Dir < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            front = -1;
        }
        else if (m_flick.Dir > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            front = 1;
        }

        return h;
    }

    void Attack(ref float front)
    {
        if (Input.GetMouseButtonDown(0) && !CurrentAttack)
        {
            GameManager.Instance.EnemysSpeed(true);
            CurrentAttack = true;
            m_rate = 2;
        }

        if (Input.GetMouseButton(0))
        {
            m_line.Drow(transform, new Vector2(front, 0) * m_attackRange);
            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_flick.IsSlide)
            {
                m_attackClass.Set(new Vector2(front, 0) * m_attackRange, gameObject, Parent.Player);
            }

            GameManager.Instance.EnemysSpeed(false);
            m_line.Des();
            m_rate = 1;
            CurrentAttack = false;
            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);
        }
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
