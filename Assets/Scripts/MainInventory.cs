using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class MainInventory : MonoBehaviour
{
    public ReactiveDictionary<Currency, int> Inventory { get; private set; } = new();

    private void Awake()
    {
        foreach(var currency in (Currency[])Enum.GetValues(typeof(Currency)))
        {
            Inventory.Add(currency, 0);
        }
    }
}
