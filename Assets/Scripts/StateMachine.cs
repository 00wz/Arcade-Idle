using System;
using System.Collections.Generic;

public class StateMachine<ContextType,DefaultState>:IStateMachine where DefaultState : BaseState<ContextType>
{
    private Dictionary<Type, BaseState<ContextType>> _statePull = new();
    private ContextType _context;
    public IState currentState { get; private set; }

    public StateMachine(ContextType context)
    {
        _context = context;
        currentState = GetState(typeof(DefaultState));
        currentState.Enter();
    }

    public void ChangeState(Type stateType)
    {
        if(stateType.BaseType!= typeof(BaseState<ContextType>))
        {
            throw new Exception($"Type: \"{stateType}\" can't be the state of the" +
                $" {typeof(StateMachine<ContextType, DefaultState>)}");
        }
        currentState.Exit();
        currentState = GetState(stateType);
        currentState.Enter();
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

    public void Dispose()
    {
        currentState.Exit();
    }
}
