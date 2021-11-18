using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IsGround,
    IsFloating,
    // Enemy
    ImpactGround,
    ImpactFloat,
}

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharaBase : MonoBehaviour, IState
{
    public int Power { protected get; set; } = 0;

    public State Current { get; private set; } = State.IsGround;
    public State ChangeState(State get) => Current = get;
}
