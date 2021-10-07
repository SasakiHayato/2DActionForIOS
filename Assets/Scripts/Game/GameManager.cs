using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    float m_rate = 0;

    bool m_setRate = false;
    public bool SetRate { private get => m_setRate; set { m_setRate = value; } }

    void Update()
    {
        if (m_setRate)
            SetTimeRate();
    }

    void SetTimeRate()
    {
        //m_rate += Time.deltaTime * 2;
        //float rate = m_curve.Evaluate(m_rate);
        
        //Time.timeScale = rate;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
}
