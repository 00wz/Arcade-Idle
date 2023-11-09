using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System.Collections;

public abstract class InteractableObject : MonoBehaviour,IInterractable, ISaveble
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
        stateMachine.Interract(character);
    }

    protected virtual void OnDestroy()
    {
        stateMachine.Dispose();
        sceneMassage.Dispose();
    }

    public ArrayList Save()
    {
        return stateMachine.Save();
    }

    public void Load(ArrayList data)
    {
        stateMachine.Load(data);
    }
}
