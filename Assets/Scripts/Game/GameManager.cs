using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeReference, SubclassSelector] 
    List<IManager> m_execution = new List<IManager>();

    bool m_setRate = false;
    public bool SetRate { private get => m_setRate; set { m_setRate = value; } }

    private void Awake()
    {
        foreach (IManager ex in m_execution)
        {
            ex.Do = false;
        }
    }

    void Update()
    {
        foreach (IManager ex in m_execution)
        {
            ex.Execution();
        }
    }
}
