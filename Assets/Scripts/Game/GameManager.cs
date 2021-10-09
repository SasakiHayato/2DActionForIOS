using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IManage;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeReference, SubclassSelector] 
    List<IManager> m_execution = new List<IManager>();

    IManageControl m_manage = new IManageControl();

    private void Awake()
    {
        foreach (IManager ex in m_execution)
        {
            if (ex == null) continue;
            m_manage.AddDic(ex);
        }
            
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    void Update()
    {
        GoSystem(Systems.CameraControl);
    }

    public void GoSystem(Systems type) => m_manage.Request(type);
}

class IManageControl
{
    Dictionary<Systems, IManager> m_dic = new Dictionary<Systems, IManager>();

    public void AddDic(IManager manager)
    {
        Systems get = FindManagerId(manager);
        m_dic.Add(get, manager);
    }

    public void Request(Systems type) => m_dic[type].Execution();

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
