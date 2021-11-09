using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    static private StateManager _instance = null;
    static public StateManager Instance => _instance;
    private StateManager() { }

    IState _other;

    private void Awake() => _instance = this;

    public static void SetState(GameObject my, IState other)
    {
        IState state = my.GetComponent<IState>();
        Instance._other = other;

        if (my.CompareTag("Player")) Instance.ToPlayer(state);
        else if (my.CompareTag("Enemy")) Instance.ToEnemy(state);
    }

    void ToPlayer(IState my)
    {
        if (my.Current == State.IsGround && _other.Current == State.IsGround)
        {

        }
    }

    void ToEnemy(IState my)
    {

    }
}
