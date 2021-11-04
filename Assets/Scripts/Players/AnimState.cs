using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimState : MonoBehaviour
{
    private static AnimState _instance;
    public static AnimState Instance
    {
        get
        {
            if (_instance == null)
            {
                object get = FindObjectOfType(typeof(AnimState));
                if (get != null)
                {
                    Debug.Log("This already has an instance.");
                    _instance = (AnimState)get;
                }
                else
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<AnimState>();
                    obj.hideFlags = HideFlags.HideInHierarchy;
                }
            }

            return _instance;
        }
    }

    Animator _animator = null;
    Action _event = null;
    string _defName = null;

    public static void SetAnimOneShot(GameObject get, string setName, string defName = null)
    {
        Instance._animator = get.GetComponent<Animator>();
        Instance._defName = defName;
        
        if (Instance._animator == null)
        {
            Debug.Log("Missing Animator. This object has not AnimatorConponent.");
            return;
        }
        else
        {
            Instance.IsPlay(setName);
        }
    }

    public static void SetAnimOneShot(GameObject get, string setName, string defName = null, Action setEvent = null)
    {
        Instance._animator = get.GetComponent<Animator>();
        Instance._defName = defName;
        Instance._event = setEvent;
        if (Instance._animator == null)
        {
            Debug.Log("Missing Animator. This object has not AnimatorConponent.");
            return;
        }
        else
        {
            Instance.IsPlay(setName);
        }
    }

    void IsPlay(string name)
    {
        Instance._animator.Play(name);
        
        StartCoroutine(IsPlaying());
    }

    IEnumerator IsPlaying()
    {
        bool isEnd = false;
        
        while (!isEnd)
        {
            AnimatorStateInfo state = Instance._animator.GetCurrentAnimatorStateInfo(0);
            
            if (state.normalizedTime >= 1)
            {
                ResetParam();
                isEnd = true;
            }
           
            yield return null;
        }
    }

    void ResetParam()
    {
        _defName = null;
        _event = null;
    }
}
