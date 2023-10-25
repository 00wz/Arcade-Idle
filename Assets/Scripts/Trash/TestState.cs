using System;
using UnityEngine;

public class TestState : BaseState<InteractableObject>
{
    public TestState(InteractableObject context, Action<Type> changeStateCallback) : base(context, changeStateCallback)
    {
    }

    public override void Interract(ICharacter character)
    {
        Debug.Log("interact with " + context.gameObject.name);
    }
}
