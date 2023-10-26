using System;

public class PurchasedState : BaseState<Container>
{
    public PurchasedState(Container context, Action<Type> changeStateCallback) : base(context, changeStateCallback)
    {
    }

    public override void Enter()
    {
        context.ShowContent();
    }
}
