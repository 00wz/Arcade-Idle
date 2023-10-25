using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private string Name;
    protected IStateMachine stateMachine;
    public SceneMassage sceneMassage { get; private set; }

    protected virtual void Awake()
    {
        sceneMassage = new SceneMassage(transform);
        sceneMassage.SetHeadMassage(Name);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<ICharacter>(out ICharacter character))
        {
            stateMachine.currentState.Interract(character);
        }
    }

    protected virtual void OnDestroy()
    {
        stateMachine.Dispose();
        sceneMassage.Dispose();
    }
}
