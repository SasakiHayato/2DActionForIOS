using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorBase : MonoBehaviour
{
    [SerializeField] float m_hp;
    [SerializeField] float m_speed;
    [SerializeField] float m_addDamage;

    public float Hp { get => m_hp;  set { m_hp = value; } }
    public float Speed { get => m_speed; set { m_speed = value; } }
    public float Add { get => m_addDamage; private set { m_addDamage = value; } }
}
