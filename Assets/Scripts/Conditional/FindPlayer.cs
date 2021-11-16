using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemyAI;

public class FindPlayer : IConditional
{
    GameObject _player = null;
    bool _check = false;
    public bool Check()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player == null) _check = false;
            else _check = true;
        }
        
        return _check;
    }
}
