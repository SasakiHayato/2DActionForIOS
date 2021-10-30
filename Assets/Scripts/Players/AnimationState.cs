using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AnimationState : MonoBehaviour
{
    private static AnimationState _instance;
    public static AnimationState Instance
    {
        get
        {
            if (_instance == null)
            {
                object get = FindObjectOfType(typeof(AnimationState));
                if (get != null)
                {
                    Debug.Log("This already has an instance.");
                    _instance = (AnimationState)get;
                }
                else
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<AnimationState>();
                    obj.hideFlags = HideFlags.HideInHierarchy;
                }
            }

            return _instance;
        }
    }

    Animator _anim = null;

    public static void SetAnim(GameObject get, string name, Action setEvent = null)
    {
        Instance._anim = get.GetComponent<Animator>();
        if (Instance._anim == null)
        {
            Debug.Log("Missing Animator. This object has not Animator.");
            return;
        }
        else
        {
            AnimationClip a = new List<AnimationClip>
                (Instance._anim.runtimeAnimatorController.animationClips).Single(c => c.name == name);
            Debug.Log(a.length);
            
            Instance.IsPlay(name, setEvent);
        }
        
    }

    void IsPlay(string name, Action setEvent)
    {
        Instance._anim.Play(name);
        StartCoroutine(IsPlaying(setEvent));
    }

    IEnumerator IsPlaying(Action setEvent = null)
    {
        int count = 0;
        bool isEnd = false;
        while (!isEnd)
        {
            count++;
            if (count > 100)
            {
                isEnd = true;
            }
            if (_anim.enabled)
            {
                //isEnd = true;
                Debug.Log("AA");
            }

            yield return null;
        }
    }
}
