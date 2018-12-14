using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private string IsMovingTriggerName = "IsMoving";

    public void Init(Animator animator, NavMeshAgent agent)
    {
        _animator = animator;
        _agent = agent;
    }

    public void Tick()
    {        
        _animator.SetBool(IsMovingTriggerName, _agent.hasPath);
    }     
}

