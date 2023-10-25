using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TestObservable : MonoBehaviour
{
    CompositeDisposable disposables = new();

    void Start()
    {
        Observable.EveryUpdate().Subscribe(x =>
        {
            Debug.Log(x);
            if (Input.GetKeyDown(KeyCode.F))
                disposables.Clear();
        }).AddTo(disposables) ;
    }
}
