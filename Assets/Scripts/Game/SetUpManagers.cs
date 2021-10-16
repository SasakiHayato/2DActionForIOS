using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AssemblyUtils
using System;
using System.Reflection;
using System.Linq;

public class SetUpManagers : MonoBehaviour
{
    AssemblyUtils m_utils = new AssemblyUtils();
    Player m_player;

    void Start()
    {
        m_player = FindObjectOfType<Player>();

        foreach (IAttack attack in m_utils.Request<IAttack>())
            m_player.IAttacks.Add(attack);
    }
}

// 任意のインターフェイスを探す
class AssemblyUtils
{
    // 呼び出し元
    public T[] Request<T>() where T : class => CreateInterfaceInstances<T>();

    /// <summary>
    /// 現在実行中のコードを格納しているアセンブリ内の指定された
    /// インターフェイスが実装されているすべての Type を返します
    /// </summary>
    public static Type[] GetInterfaces<T>()
    {
        return Assembly.GetExecutingAssembly().
            GetTypes().Where(c => c.GetInterfaces().Any(t => t == typeof(T))).ToArray();
    }

    /// <summary>
    /// 現在実行中のコードを格納しているアセンブリ内の指定された
    /// インターフェイスが実装されているすべての Type のインスタンスを作成して返します
    /// </summary>
    public static T[] CreateInterfaceInstances<T>() where T : class
        => GetInterfaces<T>().Select(c => Activator.CreateInstance(c) as T).ToArray();
}
