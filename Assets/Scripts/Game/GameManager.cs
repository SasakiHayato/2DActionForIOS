using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    private static GameManager s_instance = null;
    public static GameManager Instance
    {
        get
        {
            if (s_instance == null) s_instance = new GameManager();
            return s_instance;
        }
    }

    string _ownerName = null;
    string _saveOwner = null;
    float _ownerTime = 0;

    public static void SetTimeRate(bool set)
    {
        if (set) Time.timeScale = 0f;
        else Time.timeScale = 1;
    }

    public static bool AttackOwner(string name)
    {
        if (Instance._ownerName == null)
        {
            Instance._ownerTime = 0;
            Instance._saveOwner = name;
            Instance._ownerName = name;
            return true;
        }
        else
        {
            if (Instance._saveOwner == name)
            {
                Instance._ownerTime += Time.unscaledDeltaTime;
                Debug.Log(Instance._ownerTime);
                if (Instance._ownerTime > 10)
                {
                    DeleteOnwer();
                    return false;
                }
            }
            if (Instance._ownerName == name) return true;
            else return false;
        }
    }
    public static void DeleteOnwer() => Instance._ownerName = null;
}
