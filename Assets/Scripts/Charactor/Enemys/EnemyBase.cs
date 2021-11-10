using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : CharaBase
{
    GameObject _player;

    public int Hp { get; set; }
    public float Speed { get; set; }

    protected bool IsMove { get; private set; } = false;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        IsMove = false;
        yield return new WaitForSeconds(1f);
        IsMove = true;
    }

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
    public abstract void Move();
    public abstract void Attack();
}
