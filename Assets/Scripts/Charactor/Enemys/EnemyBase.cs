using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : CharaBase
{
    GameObject _player;
    protected Rigidbody2D RB { get; private set; }

    public int Hp { protected get; set; }
    public float Speed { get; set; }
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
    }

    public virtual void FindPlayer(Transform thisT)
    {
        Quaternion dir = Quaternion.identity;
        if (thisT.position.x > _player.transform.position.x)
        {
            dir = Quaternion.Euler(0, 180, 0);
            if (Speed > 0) Speed *= -1;
        }
        else
        {
            dir = Quaternion.Euler(0, 0, 0);
            if (Speed < 0) Speed *= -1;
        }

        thisT.localRotation = dir;
    }
    
    public virtual void Deid(GameObject target)
    {
        FieldManagement.EnemysList.Remove(target.GetComponent<IEnemys>());
        FieldManagement.FieldCharas.Remove(target);
        Player player = FindObjectOfType<Player>();
        player.DeleteIEnemy();
        Destroy(target);
    }

    public virtual void Force(Vector2 force, float power)
    {
        if (GameManager.CurrentState == GameManager.State.IsGame)
        {
            Hp--;
            if (Hp <= 0) Deid(gameObject);
        }
        RB.drag = 0;
        UIManager.UpDateScore();
        RB.AddForce(force * power, ForceMode2D.Impulse);
    }

    // AnimEvent�ŌĂяo��
    void IsAttack() => FieldManagement.SetTimeRate(true);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject get = collision.gameObject;
        
        if (get.CompareTag("Ground") && Current == State.ImpactFloat 
            || get.CompareTag("Enemy") && Current == State.ImpactFloat)
        {
            FieldManagement.ReExplosion(gameObject);
            ChangeState(State.IsGround);
        }
        else if (get.CompareTag("Enemy") && Current == State.ImpactGround)
        {
            Debug.Log("GroundImpact");
            ChangeState(State.IsGround);
        }
        if (get.CompareTag("Ground")) ChangeState(State.IsGround);
    }
}
