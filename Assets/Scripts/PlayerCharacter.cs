using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ICharacter,ISaveble
{
    public IDictionary<Currency, int> inventary => GameRootInstance.Instance.MainInventory.Inventory;

    private bool _isWaits=false;
    private CompositeDisposable _disposables = new CompositeDisposable();

    public void Load(ArrayList saveParam)
    {
        transform.position = new Vector3((float)saveParam[0], (float)saveParam[1], (float)saveParam[2]);
    }

    public ArrayList Save()
    {
        ArrayList saveParam = new();
        saveParam.Add(transform.position.x);
        saveParam.Add(transform.position.y);
        saveParam.Add(transform.position.z);
        return saveParam;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isWaits)
        {
            return;
        }

        if (other.TryGetComponent<IInterractable>(out IInterractable interractable))
        {
            interractable.Interract(this);
            Wait(1 / interractable.InterractableSpeed);
        }
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

    private void OnDestroy()
    {
        _disposables.Clear();
    }
}
