using UnityEngine;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainInventory : MonoBehaviour,ISaveble
{
    public ReactiveDictionary<Currency, int> Inventory { get; private set; } = new();

    private void Awake()
    {
        foreach(var currency in (Currency[])Enum.GetValues(typeof(Currency)))
        {
            Inventory.Add(currency, 0);
        }
    }

    public void Load(ArrayList saveParam)
    {
        var serializableInventory = (Dictionary<Currency, int>)saveParam[0];
        Inventory.Clear();
        foreach(var item in serializableInventory)
        {
            Inventory.Add(item.Key, item.Value);
        }
    }

    public ArrayList Save()
    {
        ArrayList saveParam = new();
        saveParam.Add(new Dictionary<Currency, int>(Inventory));
        return saveParam;
    }
}
