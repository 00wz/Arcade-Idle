using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractableProxy : MonoBehaviour,IInterractable
{
    [SerializeField]
    private InteractableObject Target;

    public void Interract(ICharacter character)
    {
        Target.Interract(character);
    }
}
