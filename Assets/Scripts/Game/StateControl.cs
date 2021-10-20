using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public enum StateType
    {
        IsGround,
        IsFloating,
    }

    public class StateControl
    {
        StateType _type;

        public void SetUp() => _type = StateType.IsGround;

        public StateType CurrentState { get; }
    }

}