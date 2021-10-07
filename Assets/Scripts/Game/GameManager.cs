using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SystemType;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeReference, SubclassSelector] 
    List<IManager> m_execution = new List<IManager>();

    IManageControl m_manage = new IManageControl();

    bool m_setRate = false;
    public bool SetRate { private get => m_setRate; set { m_setRate = value; } }

    private void Awake()
    {
        foreach (IManager ex in m_execution)
        {
            ex.Do = false;
            SetDicId(ex);
        }
            

        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    void SetDicId(IManager manager)
    {
        m_manage.AddDic(manager);
    }

    void Update()
    {
        foreach (IManager ex in m_execution)
            ex.Execution();
    }

    public void DoSet(bool set, Systems type)
    {
        int id = (int)type;
        m_execution[id].Do = set;
    }
}

class IManageControl
{
    Dictionary<Systems, IManager> m_dic = new Dictionary<Systems, IManager>();

    public void AddDic(IManager manager)
    {
        Systems a = FindManagerId(manager);
        Debug.Log(a);
    }

    Systems FindManagerId(IManager manager)
    {
        string manageName = manager.ToString();
        Systems type = Systems.None;
        bool loop = true;

        int count = 0;
        while (loop)
        {
            type = (Systems)count;
            string typeName = System.Enum.GetName(typeof(Systems), type);

            if (typeName == "None") return Systems.None;
            if (manageName == typeName) loop = false;
            
            count++;
        }

        return type;
    }
}
