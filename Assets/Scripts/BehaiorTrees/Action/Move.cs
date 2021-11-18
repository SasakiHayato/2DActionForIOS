using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class Move : IAction
{
    [SerializeField] GameObject _my;
    float _speed;

    GameObject _player = null;
    Rigidbody2D _rb = null;
    bool _setUp = false;

    bool _check = false;
    public bool Reset { set { _check = value; } }
    public void Action()
    {
        if (!_setUp)
        {
            _setUp = true;
            _rb = _my.GetComponent<Rigidbody2D>();
            _speed = _my.GetComponent<EnemyBase>().Speed;
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        if (_my.transform.position.x < _player.transform.position.x)
            if (_speed > 0) _speed *= -1;

        if (_my.transform.position.x >= _player.transform.position.x)
            if (_speed < 0) _speed *= -1;

        _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        _check = true;
    }

    public bool End() => _check;
}
