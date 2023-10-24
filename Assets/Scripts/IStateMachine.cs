using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine : IDisposable
{
    public IState currentState { get; }

    public void ChangeState(Type stateType);
}
