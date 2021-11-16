using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : CharaBase
{
    GameObject _player;
    protected Rigidbody2D RB { get; private set; }
    protected bool IsMove { get; private set; } = false;
    public int Hp { get; set; }
    public float Speed { get; set; }
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
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
    
    public virtual void Deid(GameObject target)
    {
        FieldManagement.EnemysList.Remove(target.GetComponent<IEnemys>());
        FieldManagement.FieldCharas.Remove(target);
        Player player = FindObjectOfType<Player>();
        player.SetIEnemy();
        Destroy(target);
    }

    public virtual void Force(Vector2 force, float power) => RB.AddForce(force * power, ForceMode2D.Impulse);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject get = collision.gameObject;
        CharaBase chara = get.GetComponent<CharaBase>();
        
        if (get.CompareTag("Ground") || get.CompareTag("Enemy")) ChangeState(State.IsGround);
    }
}
