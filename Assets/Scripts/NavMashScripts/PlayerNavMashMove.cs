using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNavMashMove : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 2f;
    private NavMeshAgent _navMeshAgent;
    private float _diagonal—oefficient = 1 / Mathf.Sqrt(2f);
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey)
        {
            return;
        }

        Vector3 direction = Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical")*Vector3.forward;
        if (direction.sqrMagnitude > 1.5f)
        {
            direction *= _diagonal—oefficient;
        }
        _navMeshAgent.Move(direction * Time.deltaTime*MoveSpeed);
    }
}
