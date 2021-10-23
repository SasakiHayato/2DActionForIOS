using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharaBase : MonoBehaviour, IState
{
    public State Current { get; private set; } = State.IsGround;
    public virtual void ChangeState()
    {
        switch (Current)
        {   
            case State.IsGround:
                Current = State.IsFloating;
                break;
            case State.IsFloating:
                Current = State.IsGround;
                break;
            default:
                break;
        }
    }

    public abstract void Attack(State state);
}
