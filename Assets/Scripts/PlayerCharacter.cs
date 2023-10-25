using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ICharacter
{
    public IDictionary<Currency, int> inventary => GameRootInstance.Instance.MainInventory.Inventory;
}
