using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMassage:IDisposable
{
    private SceneMassageView _view;
    private Transform _target;
    private float _heightOffsetPx;

    public SceneMassage(Transform target, float heightOffsetPx = 100f)
    {
        _target = target;
        _heightOffsetPx = heightOffsetPx;
        _view= GameObject.Instantiate<SceneMassageView>(Resources.Load<SceneMassageView>("SceneMassageView"), 
            GameRootInstance.Instance.Canvas.transform);
    }

    public void Dispose()
    {
        GameObject.Destroy(_view);
    }
}
