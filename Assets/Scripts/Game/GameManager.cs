using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public enum State
    {
        IsGame,
        EndGame,
        Title,
        Tutorial,

        None,
    }

    public enum Phase
    {
        Phase1,
        Phase2,
        Phase3,

        None,
    }

    private static GameManager s_instance = null;
    public static GameManager Instance
    {
        get
        {
            if (s_instance == null) s_instance = new GameManager();
            return s_instance;
        }
    }

    Phase _savePhase = Phase.None;

    public static State CurrentState { get; private set; } = State.None;
    public static void ChangeState(State state) => CurrentState = state;

    public static Vector2 CurrentPlayerPos { get; private set; }
    public static void SetPlayerPos(Vector2 set) => CurrentPlayerPos = set;

    public static bool IsSetting { get; private set; } = false;
    public static bool Setting(bool set) => IsSetting = set;

    public static Phase CurrentPhase { get; private set; } = Phase.None;
    public static void CheckPhase(float time)
    {
        
    }
}
