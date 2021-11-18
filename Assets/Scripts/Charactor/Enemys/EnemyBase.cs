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
        RB.drag = 0;
        RB.AddForce(force * power, ForceMode2D.Impulse);
    }

    // AnimEvent‚ÅŒÄ‚Ño‚µ
    void IsAttack() => GameManager.SetTimeRate(true);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) ChangeState(State.IsGround);
    }
}
