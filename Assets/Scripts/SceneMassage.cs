using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SceneMassage:IDisposable
{
    private SceneMassageView _view;
    private Transform _target;
    private float _heightOffsetPx;
    private CompositeDisposable _disposables;
    private Camera _camera;

    public SceneMassage(Transform target, float heightOffsetPx = 100f)
    {
        _target = target;
        _heightOffsetPx = heightOffsetPx;
        _camera = Camera.main;
        _view= GameObject.Instantiate<SceneMassageView>(Resources.Load<SceneMassageView>("SceneMassageView"), 
            GameRootInstance.Instance.Canvas.transform);
        Observable.EveryUpdate().Subscribe(_ => ShowMassage()).AddTo(_disposables);
    }

    public void SetHeadMassage(string massage)
    {
        _view.SetHeadMassage(massage);
    }

    public void SetBodyMassage(string massage)
    {
        _view.SetBodyMassage(massage);
    }

    private void ShowMassage()
    {
        if(TryGetScreenPoint(_target,out Vector3 position))
        {
            if (!_view.gameObject.activeSelf)
            {
                _view.gameObject.SetActive(true);
            }
            _view.transform.position = position;
        }
        else
        {
            if (_view.gameObject.activeSelf)
            {
                _view.gameObject.SetActive(false);
            }
        }
    }

    private bool TryGetScreenPoint(Transform target,out Vector3 position)
    {
        position = _camera.WorldToScreenPoint(target.position);
        var isVisible= position.x > 0 && position.x < Screen.width &&
            position.y > 0 && position.y < Screen.height &&
            position.z > 0;
        position.y += _heightOffsetPx;
        return isVisible;
    }

    public void Dispose()
    {
        GameObject.Destroy(_view);
        _disposables.Clear();
    }
}
