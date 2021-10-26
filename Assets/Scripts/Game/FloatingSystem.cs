using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSystem
{
    GameObject _force;

    public void Set(GameObject target)
    {
        if (_force == null)
            _force = GameObject.Find("Force");
        
        target.transform.position = _force.transform.position;
    }
}

