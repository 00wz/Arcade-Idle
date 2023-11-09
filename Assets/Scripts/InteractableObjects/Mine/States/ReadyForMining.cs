using System;

public class ReadyForMining : BaseState<Mine>
{
    private int _currentReserve;
    public ReadyForMining(Mine context, Action<Type> changeStateCallback) :
        base(context, changeStateCallback)
    {
    }

    public override void Enter()
    {
        _currentReserve = context.MaximumCapacity;
        context.sceneMassage.SetBodyMessage($"{context.MinedCurrency} left: {_currentReserve}");
    }

    public override void Interract(ICharacter character)
    {
        GiveResource(character);
        if (_currentReserve <= 0)
        {
            ChangeState<ReloadMine>();
            return;
        }
    }

    private void GiveResource(ICharacter character)
    {
        _currentReserve--;
        context.sceneMassage.SetBodyMessage($"{context.MinedCurrency} left: {_currentReserve}");
        character.inventary[context.MinedCurrency]++;
    }
}
