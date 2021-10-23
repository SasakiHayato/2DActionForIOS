using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    static private AttackSystem _instance;
    static public AttackSystem Attack
    {
        get
        {
            AttackSystem attack = FindObjectOfType<AttackSystem>();
            if (attack == null)
            {
                GameObject obj = new GameObject("AttackSytem");
                _instance = obj.AddComponent<AttackSystem>();
            }

            return _instance;
        }
    }

    public void Set(GameObject parent)
    {
        Debug.Log("aa");
    }
}


