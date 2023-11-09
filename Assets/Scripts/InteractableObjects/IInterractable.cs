using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterractable
{
    public float InterractableSpeed { get; }
    public void Interract(ICharacter character);
}
