using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemyType;

public class EnemyBase : MonoBehaviour
{
    EnemyData m_data = new EnemyData();
    [SerializeField] Type m_type;

    float m_hp;
    float m_speed;

    public float Hp { get => m_hp; set { m_hp = value; } }
    public float Speed { get => m_speed; private set { m_speed = value; } }

    private void Awake() => m_data.Set(m_type,ref m_hp,ref m_speed);

    public void Died()
    {
        Debug.Log("EnemyŽ€‚ñ‚¾");
    }
}

public class EnemyData
{
    public void Set(Type type,ref float hp,ref float speed)
    {
        switch (type)
        {
            case Type.A:
                DataA(ref hp,ref speed);
                break;
            case Type.B:
                DataB(ref hp, ref speed);
                break;
            case Type.C:
                DataC(ref hp, ref speed);
                break;
            default:
                break;
        }
    }

    void DataA(ref float hp,ref float speed)
    {
        hp = 1;
        speed = 2;
    }

    void DataB(ref float hp, ref float speed)
    {
        hp = 2;
        speed = 4;
    }

    void DataC(ref float hp, ref float speed)
    {
        hp = 4;
        speed = 2;
    }
}

