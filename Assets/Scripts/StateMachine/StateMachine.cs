using System;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<ContextType,DefaultState>:IStateMachine where DefaultState : BaseState<ContextType>
{
    private Dictionary<Type, BaseState<ContextType>> _statePull = new();
    private ContextType _context;
    private BaseState<ContextType> _currentState;

    public StateMachine(ContextType context)
    {
        _context = context;
        _currentState = GetState(typeof(DefaultState));
        _currentState.Enter();
    }

    public void Interract(ICharacter character) => _currentState.Interract(character);

    public void Load(ArrayList data)
    {
        ChangeState((Type)data[0]);
        _currentState.Load((ArrayList)data[1]);
    }

    public ArrayList Save()
    {
        ArrayList saveParam = new();
        saveParam.Add(_currentState.GetType());
        saveParam.Add(_currentState.Save());
        return saveParam;
    }

    private void ChangeState(Type stateType)
    {
        if(stateType.BaseType!= typeof(BaseState<ContextType>))
        {
            throw new Exception($"Type: \"{stateType}\" can't be the state of the" +
                $" {typeof(StateMachine<ContextType, DefaultState>)}");
        }
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
        BaseState<ContextType> newState = (BaseState<ContextType>)Activator.CreateInstance(
            stateType,_context,new Action<Type>(ChangeState));
        _statePull[stateType] = newState;
        return newState;
    }

    public void Dispose()
    {
        _currentState.Exit();
    }
}
