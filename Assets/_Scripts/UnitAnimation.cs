using UnityEngine;
using UnityEngine.AI;

public class UnitAnimation : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private string IsMovingTriggerName = "IsMoving";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        /// TODO: попробывать обновлять через подписку на событие изменения значения, а не вот этот весь FixedUpdate
        _animator.SetBool(IsMovingTriggerName, _agent.hasPath);
    } 
}

