using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected IStateMachine stateMachine;
    protected SceneMassage sceneMassage { get; private set; }

    protected virtual void Awake()
    {
        sceneMassage = new SceneMassage(transform);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<ICharacter>(out ICharacter character))
        {
            stateMachine.currentState.Interract(character);
            Debug.Log((stateMachine.currentState).GetType());
        }
    }

    protected virtual void OnDestroy()
    {
        stateMachine.Dispose();
        sceneMassage.Dispose();
    }
}
