using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GetEnumToGame;

public class GameMaster
{
    // Singleton
    static GameMaster m_instance = new GameMaster();
    public static GameMaster Instance() => m_instance;
    private GameMaster() { }

    static bool m_isPlay;
    public bool Play { get => m_isPlay; set { m_isPlay = value; } }

    static Game m_type = Game.Titel;
    public Game CurrentType { get => m_type; private set { m_type = value; } }

    public void IsSceneCheck(Game type) => CurrentType = type;
}
