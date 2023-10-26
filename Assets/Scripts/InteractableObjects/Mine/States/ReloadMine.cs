using System;
using UniRx;
using UnityEngine;

public class ReloadMine : BaseState<Mine>
{
    private float _remainingTime;
    private CompositeDisposable _disposables = new CompositeDisposable();
    public ReloadMine(Mine context, Action<Type> changeStateCallback) : base(context, changeStateCallback)
    {
    }
    public override void Enter()
    {
        _remainingTime = context.ReloadTime;
        context.sceneMassage.SetBodyMessage($"remaining: {_remainingTime.ToString("f1")}");
        Observable.EveryUpdate().Subscribe(_ => TimerTick()).AddTo(_disposables);
    }

    private void TimerTick()
    {
        _remainingTime -= Time.deltaTime;
        if (_remainingTime <= 0f)
        {
            _disposables.Clear();
            ChangeState<ReadyForMining>();
            return;
        }
        context.sceneMassage.SetBodyMessage($"remaining: {_remainingTime.ToString("f1")}");
    }
    public override void Exit()
    {
        _disposables.Clear();
    }
}
