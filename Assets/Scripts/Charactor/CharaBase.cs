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
    public int Power { protected get; set; } = 0;

    public State Current { get; set; } = State.IsGround;
    public virtual State ChangeState(State get) => Current = get;

    public abstract void AttackMove(AttackSetting.ActionType type, int combo = 0);
}
