using System;
using System.Collections.Generic;

public class StateMachine<ContextType,DefaultState> where DefaultState : BaseState<ContextType>
{
    private Dictionary<Type, BaseState<ContextType>> _statePull = new();
    private ContextType _context;
    public BaseState<ContextType> _currentState { get; private set; }

    public StateMachine(ContextType context)
    {
        _context = context;
        _currentState = GetState(typeof(DefaultState));
        _currentState.Enter();
    }

    public void ChangeState<T>() where T : BaseState<ContextType>
    {
        ChangeState(typeof(T));
    }

    private void ChangeState(Type stateType)
    {
        _currentState.Exit();
        _currentState = GetState(stateType);
        _currentState.Enter();
    }

    private BaseState<ContextType> GetState(Type stateType)
    {
        if(_statePull.TryGetValue(stateType,out BaseState<ContextType> result))
        {
            return result;
        }
        BaseState<ContextType> newState = 
            (BaseState<ContextType>)Activator.CreateInstance(stateType,_context,new Action<Type>(ChangeState));
        _statePull[stateType] = newState;
        return newState;
    }
}
