using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

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
        _isWaits = true;
        Observable.Timer(TimeSpan.FromSeconds(1 / context.MiningSpeed)).Subscribe(_ =>
        {
            _isWaits = false;
            _disposables.Clear();
        }).AddTo(_disposables);
    }
    private void GiveResource(ICharacter character)
    {
        _currentReserve--;
        context.sceneMassage.SetBodyMassage($"{context.MinedCurrency} left: {_currentReserve}");
        //Debug.Log($"_currentReserve = {_currentReserve}");
    }
    public override void Exit()
    {
        _disposables.Clear();
    }
}
