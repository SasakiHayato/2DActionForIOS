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

    State _state = State.None;
    public static State CurrentState { get => Instance._state; }
    public static void ChangeState(State state) => Instance._state = state;
}
