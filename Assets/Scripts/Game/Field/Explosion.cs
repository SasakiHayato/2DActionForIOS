using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    GameObject _target = null;
    public GameObject Target { set { _target = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

        if (enemy != null && enemy.name != _target.name) SetForce(enemy);
        Des();
    }

    void Des()
    {
        _target.GetComponent<EnemyBase>().Deid(_target);
        Destroy(gameObject);
    }

    void SetForce(EnemyBase enemy)
    {
        Vector2 enemyVec = enemy.transform.position;
        float angle = 0;

        if (enemyVec.x < transform.position.x) angle = 135;
        else angle = 45;

        float rad = angle * (Mathf.PI / 180);
        enemy.ChangeState(State.None);
        enemy.Force(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)), 30);
    }
}
