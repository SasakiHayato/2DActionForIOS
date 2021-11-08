using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCtrlToPlayer : MonoBehaviour
{   
    IDamageble _damageble;
    IState _state;
    CharaBase _chara;

    void Start()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Player");
        _damageble = parent.GetComponent<IDamageble>();
        _state = parent.GetComponent<IState>();
        _chara = parent.GetComponent<CharaBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
        IState state = collision.gameObject.GetComponent<IState>();

        if (damageble == null || state == null) return;

        ChackState(state, collision.gameObject);

        int add = _damageble.AddDamage();
        damageble.GetDamage(add);
    }

    void ChackState(IState other, GameObject enemy)
    {
        if (_state.Current == State.IsGround && other.Current == State.IsGround)
        {
            other.ChangeState(State.IsFloating);
            AttackSystems.SetEnemy(enemy);
        }
        else if (_state.Current == State.IsGround && other.Current == State.IsFloating)
        {
            _chara.ChangeState(State.IsFloating);
            AttackSystems.MoveAttack(gameObject, 2);
        }
        else if (_state.Current == State.IsFloating && other.Current == State.IsFloating)
        {
            _chara.ChangeState(State.IsGround);
            AttackSystems.MoveAttack(gameObject, -4);
            AttackSystems.DeleteList();
        }
    }
}
