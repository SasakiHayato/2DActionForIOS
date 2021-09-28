using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : CharactorBase, IDamageble
{
    float m_defsSpeed;

    public int ThisID { get; set; }
    // Start‚Åˆê“xéŒ¾
    public float DefSpeed { get => m_defsSpeed; set { m_defsSpeed = value; } }

    public float AddDamage() => Add;
    public void GetDamage(float damage)
    {
        Hp -= damage;
        
        if (Hp <= 0)
            Deid();
    }
    public void ThisMotionSpeed(float rate, bool set)
    {
        if (set)
            Speed /= rate;
        else
            Speed = DefSpeed;
    }

    public Quaternion SetTrans(Transform set, float speed)
    {
        if (speed < 0)
            set.localRotation = Quaternion.Euler(0, 180, 0);
        else if (speed > 0)
            set.localRotation = Quaternion.Euler(0, 0, 0);

        return set.localRotation;
    }

    public abstract void Attack();
    public abstract void Move();
    public abstract void Deid();
}
