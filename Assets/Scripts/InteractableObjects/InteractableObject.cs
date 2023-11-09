using UnityEngine;
using UniRx.Triggers;
using UniRx;

public abstract class InteractableObject : MonoBehaviour,IInterractable
{
    [SerializeField]
    protected string Name;
    [SerializeField]
    private float InterractableSpeed;

    float IInterractable.InterractableSpeed => InterractableSpeed;
    public SceneMessage sceneMassage { get; protected set; }


    protected IStateMachine stateMachine;

    protected virtual void Awake()
    {
        sceneMassage = new SceneMessage(transform);
        sceneMassage.SetHeadMessage(Name);
    }

    public void Interract(ICharacter character)
    {
        stateMachine.currentState.Interract(character);
    }

    protected virtual void OnDestroy()
    {
        stateMachine.Dispose();
        sceneMassage.Dispose();
    }
}
