using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : InteractableObject
{
    [SerializeField]
    public Currency MinedCurrency;

    [SerializeField]
    public int MaximumCapacity;

    [SerializeField]
    public float MiningSpeed;

    [SerializeField]
    public float ReloadTime;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<Mine, ReadyForMining>(this);
    }
}
