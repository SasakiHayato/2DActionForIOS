using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    Animation _anim;
    
    void Start()
    {
        _anim = GetComponent<Animation>();
        Debug.Log(_anim.GetClipCount());
        foreach (AnimationState item in _anim)
        {
            
        }

        
    }
}
