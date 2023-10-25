using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public IDictionary<Currency, int> inventary { get; }
}
