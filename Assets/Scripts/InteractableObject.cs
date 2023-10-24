using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private StateMachine<InteractableObject, TestState> _stateMachine;

    private void Start()
    {
        _stateMachine = new StateMachine<InteractableObject, TestState>(this);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<PlayerNavMashMove>(out _))
        {
            _stateMachine._currentState.Interract();
        }
    }
}
