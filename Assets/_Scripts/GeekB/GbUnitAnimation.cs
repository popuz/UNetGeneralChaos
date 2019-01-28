using System;
using UnityEngine;
using UnityEngine.AI;

public class GbUnitAnimation : MonoBehaviour
{
    private static readonly int MoveTriggerHash = Animator.StringToHash("Move");

    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        if(_animator == null)
            _animator = GetComponent<Animator>();
        if(_agent == null)
            _agent = GetComponentInParent<NavMeshAgent>();
    }

    private void FixedUpdate() => _animator.SetBool(MoveTriggerHash, Math.Abs(_agent.velocity.magnitude) > float.Epsilon);
}