using System;
using UniRx;

public class ReadyForMining : BaseState<Mine>
{
    private int _currentReserve;
    private bool _isWaits;
    private CompositeDisposable _disposables = new CompositeDisposable();
    public ReadyForMining(Mine context, Action<Type> changeStateCallback) :
        base(context, changeStateCallback)
    {
    }

    public override void Enter()
    {
        _currentReserve = context.MaximumCapacity;
        context.sceneMassage.SetBodyMassage($"{context.MinedCurrency} left: {_currentReserve}");
        _isWaits = false;
    }

    public override void Interract(ICharacter character)
    {
        if (_isWaits)
        {
            return;
        }
        GiveResource(character);
        if (_currentReserve <= 0)
        {
            ChangeState<ReloadMine>();
            return;
        }
        Wait(1 / context.MiningSpeed);
    }

    private void Wait(float waitTime)
    {
        _isWaits = true;
        Observable.Timer(TimeSpan.FromSeconds(waitTime)).Subscribe(_ =>
        {
            _isWaits = false;
            _disposables.Clear();
        }).AddTo(_disposables);
    }

    private void GiveResource(ICharacter character)
    {
        _currentReserve--;
        context.sceneMassage.SetBodyMassage($"{context.MinedCurrency} left: {_currentReserve}");
        character.inventary[context.MinedCurrency]++;
    }

    public override void Exit()
    {
        _disposables.Clear();
    }
}
