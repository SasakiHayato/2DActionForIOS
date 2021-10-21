using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    GameObject _player;

    public int Hp { get; set; }
    public float Speed { get; set; }
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public abstract void Move();
    public virtual void FindPlayer(Transform thisT)
    {
        Quaternion dir = Quaternion.identity;
        if (thisT.position.x > _player.transform.position.x)
        {
            dir = Quaternion.Euler(0, 180, 0);
            if (Speed > 0)
                Speed *= -1;
        }
        else
        {
            dir = Quaternion.Euler(0, 0, 0);
            if (Speed < 0)
                Speed *= -1;
        }

        thisT.localRotation = dir;
    }
}
