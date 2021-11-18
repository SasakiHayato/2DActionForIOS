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

    public static void SetTimeRate(bool set)
    {
        if (set) Time.timeScale = 0f;
        else Time.timeScale = 1;
    }
}
