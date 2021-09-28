using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GetEnumToGame;

public class PlayerController : CharactorBase, IDamageble
{
    Rigidbody2D m_rb;
    AttackClass m_attackClass = new AttackClass();
    DrawLine m_line = new DrawLine();

    float m_rate = 1;
    public bool CurrentAttack { get; private set; }

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        CurrentAttack = false;
    }

    void Update()
    {
        float front = 0;
        float h = Move(ref front);
        Attack(ref front);

        m_rb.velocity = new Vector2(h / m_rate, m_rb.velocity.y);
    }

    float Move(ref float front)
    {
        float h = Input.GetAxisRaw("Horizontal") * Speed;

        if (h < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            front = -1;
        }
        else if (h > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            front = 1;
        }

        return h;
    }
    void Attack(ref float front)
    {
        if (Input.GetButtonDown("Fire1") && !CurrentAttack)
        {
            GameManager.Instance.EnemysSpeed(true);
            CurrentAttack = true;
            m_rate = 2;
        }

        if (Input.GetButton("Fire1"))
        {
            m_line.Drow(transform, new Vector2(front, 0) * 4);
            GameManager.Instance.SetUiParam(UiType.Ui.PlayerSlider);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            m_attackClass.Set(new Vector2(front, 0) * 4, gameObject, Parent.Player, AttackType.Type.Shlash);
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
