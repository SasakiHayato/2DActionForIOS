using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IsGround,
    IsFloating,
}

public abstract class CharaBase : MonoBehaviour, IState
{
    public State Current { get; private set; } = State.IsGround;
    public virtual State ChangeState(State get) => Current = get;
}
