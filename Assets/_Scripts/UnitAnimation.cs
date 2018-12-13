using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private NavMeshAgent _agent;

    private string IsMovingTriggerName = "IsMoving";
    void FixedUpdate()
    {
        if (!_agent.hasPath)
        {
            _animator.SetBool(IsMovingTriggerName, false);
        }
        else
        {
            _animator.SetBool(IsMovingTriggerName, true);
        }
    } 
}

