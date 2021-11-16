using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTrees;

public class NormalAttack : IAction
{
    [SerializeField] GameObject _my;
    [SerializeField] string _animName;

    Animator _anim = null;
    bool _isPlay = false;

    bool _check = false;
    public bool Reset { set { _check = value; } }

    public void Action()
    {
        if (_anim == null) _anim = _my.GetComponent<Animator>();
        if (!_isPlay)
        {
            _isPlay = true;
            _anim.Play(_animName);
        }
        
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            _isPlay = false;
            _check = true;
        }
    }

    public bool End() => _check;
}