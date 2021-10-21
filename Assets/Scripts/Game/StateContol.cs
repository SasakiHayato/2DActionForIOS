using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{ 
    IsGround,
    IsFloating,
}

public class StateContol
{
    State _state = State.IsGround;

    public State Current { get => _state; }
    public void ChangeState()
    {

    }
}
