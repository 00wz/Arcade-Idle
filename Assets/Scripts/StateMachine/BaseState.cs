using System;
using UnityEngine;

public abstract class BaseState<ContextType>:IState
{
    protected ContextType context;
    private Action<Type> _changeState;

    public BaseState(ContextType context, Action<Type> changeStateCallback)
    {
        this.context = context;
        _changeState = changeStateCallback;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Interract(ICharacter character)
    {
    }

    protected void ChangeState<NewState>()where NewState: BaseState<ContextType>
    {
        _changeState.Invoke(typeof(NewState));
    }
}
