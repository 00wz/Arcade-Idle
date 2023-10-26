using System;
using UniRx;
using UnityEngine;
using UniRx.Triggers;

public class SceneMessage:IDisposable
{
    private SceneMessageView _view;
    private Transform _target;
    private float _heightOffsetPx;
    private CompositeDisposable _targetSubscriptions=new CompositeDisposable();
    private CompositeDisposable _updateSubscriptions = new CompositeDisposable();
    private Camera _camera;
    private const string RESOURCE_PATH = "SceneMessageView";

    private bool enabled
    {
        set
        {
            if (value)
            {
                Observable.EveryUpdate().Subscribe(_ => ShowMessage()).AddTo(_updateSubscriptions);
            }
            else
            {
                _updateSubscriptions.Clear();
                if(_view)
                    _view.gameObject.SetActive(false);
            }
        }
    }

    public SceneMessage(Transform target, float heightOffsetPx = 100f)
    {
        _target = target;
        _heightOffsetPx = heightOffsetPx;
        _camera = Camera.main;
        _view= GameObject.Instantiate<SceneMessageView>(Resources.Load<SceneMessageView>(RESOURCE_PATH), 
            GameRootInstance.Instance.Canvas.transform);
        this.enabled = target.gameObject.activeInHierarchy;
        target.OnEnableAsObservable().Subscribe(_ => enabled=true).AddTo(_targetSubscriptions);
        target.OnDisableAsObservable().Subscribe(_ => enabled=false).AddTo(_targetSubscriptions);
    }

    public void SetHeadMessage(string massage)
    {
        _view.SetHeadMessage(massage);
    }

    public void SetBodyMessage(string massage)
    {
        _view.SetBodyMessage(massage);
    }

    private void ShowMessage()
    {
        if (TryGetScreenPoint(_target, out Vector3 position))
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
        _updateSubscriptions.Clear();
        _targetSubscriptions.Clear();
    }
}
