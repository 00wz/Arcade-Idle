using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ICharacter,ISaveble
{
    public IDictionary<Currency, int> inventary => GameRootInstance.Instance.MainInventory.Inventory;

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
}
