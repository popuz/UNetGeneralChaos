using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

}
