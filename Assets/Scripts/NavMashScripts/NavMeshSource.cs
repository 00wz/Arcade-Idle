using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshSource : MonoBehaviour
{
    private void OnEnable()
    {
        GameRootInstance.Instance?.NavMashUpdater.UpdateNavMash();
    }

    private void OnDisable()
    {
        GameRootInstance.Instance?.NavMashUpdater.UpdateNavMash();
    }
}
