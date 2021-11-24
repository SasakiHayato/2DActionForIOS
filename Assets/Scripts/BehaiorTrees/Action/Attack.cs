using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorAI;

public class Attack : IAction
{
    [SerializeField] string _animName;

    Animator _anim = null;
    bool _isPlay = false;

    bool _check = false;
    public bool Reset { set { _check = value; } }

    public void Execute()
    {
        if (!FieldManagement.AttackOwner(Target.name)) return;
        if (_anim == null) _anim = Target.GetComponent<Animator>();

        if (!_isPlay)
        {
            _isPlay = true;
            _anim.Play(_animName);
        }
        
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            FieldManagement.DeleteOnwer();
            _isPlay = false;
            _check = true;
        }
    }

    public bool End() => _check;
    public GameObject Target { private get; set; }
}
