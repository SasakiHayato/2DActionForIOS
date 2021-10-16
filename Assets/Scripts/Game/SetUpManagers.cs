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

// �C�ӂ̃C���^�[�t�F�C�X��T��
class AssemblyUtils
{
    // �Ăяo����
    public T[] Request<T>() where T : class => CreateInterfaceInstances<T>();

    /// <summary>
    /// ���ݎ��s���̃R�[�h���i�[���Ă���A�Z���u�����̎w�肳�ꂽ
    /// �C���^�[�t�F�C�X����������Ă��邷�ׂĂ� Type ��Ԃ��܂�
    /// </summary>
    public static Type[] GetInterfaces<T>()
    {
        return Assembly.GetExecutingAssembly().
            GetTypes().Where(c => c.GetInterfaces().Any(t => t == typeof(T))).ToArray();
    }

    /// <summary>
    /// ���ݎ��s���̃R�[�h���i�[���Ă���A�Z���u�����̎w�肳�ꂽ
    /// �C���^�[�t�F�C�X����������Ă��邷�ׂĂ� Type �̃C���X�^���X���쐬���ĕԂ��܂�
    /// </summary>
    public static T[] CreateInterfaceInstances<T>() where T : class
        => GetInterfaces<T>().Select(c => Activator.CreateInstance(c) as T).ToArray();
}
