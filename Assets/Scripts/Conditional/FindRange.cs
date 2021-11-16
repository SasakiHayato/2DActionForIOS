using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class FindRange : IConditional
{
    [SerializeField] float _range;
    [SerializeField] Transform _my;
    GameObject _player = null;
    bool _check = false;

    public bool Check()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector2.Distance(_my.position, _player.transform.position);
        
        if (distance < _range) _check = true;
        else _check = false;

        return _check;
    }

}
