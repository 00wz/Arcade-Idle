using UnityEngine;
using UniRx.Triggers;
using UniRx;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]
    protected string Name;
    public SceneMessage sceneMassage { get; protected set; }
    protected IStateMachine stateMachine;
    protected CompositeDisposable _disposables = new();

    protected virtual void Awake()
    {
        sceneMassage = new SceneMessage(transform);
        sceneMassage.SetHeadMessage(Name);
        this.OnTriggerStayAsObservable().Subscribe(collider =>TriggerStay(collider)).AddTo(_disposables);
    }

    protected virtual void TriggerStay(Collider other)
    {
        if(other.TryGetComponent<ICharacter>(out ICharacter character))
        {
            stateMachine.currentState.Interract(character);
        }
    }

    protected virtual void OnDestroy()
    {
        _disposables.Clear();
        stateMachine.Dispose();
        sceneMassage.Dispose();
    }
}
