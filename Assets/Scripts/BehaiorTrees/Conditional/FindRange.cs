using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorAI;

public class FindRange : IConditional
{
    [SerializeField] Vector2 _range = Vector2.zero;
    GameObject _player = null;
    bool _check = false;

    public bool Check()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        float x = Target.transform.position.x - _player.transform.position.x;
        float y = Target.transform.position.y - _player.transform.position.y;

        if (Mathf.Abs(x) < _range.x && Mathf.Abs(y) < _range.y) _check = true;
        else _check = false;

        return _check;
    }

    public GameObject Target { private get; set; }
}
