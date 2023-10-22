using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMashUpdater : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;
    private bool _IsUpdateNow=false;

    private void Start()
    {
        _navMeshSurface=GetComponent<NavMeshSurface>();
    }

    public void UpdateNavMash()
    {
        if (!_IsUpdateNow)
        {
            _IsUpdateNow = true;
            StartCoroutine(UpdateNavMashEndOfFrame());
        }
    }

    private IEnumerator UpdateNavMashEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        _IsUpdateNow = false;
    }
}
