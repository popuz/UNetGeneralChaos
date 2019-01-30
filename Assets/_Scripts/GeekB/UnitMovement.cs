using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;

    private void Start() => _agent = GetComponent<NavMeshAgent>();
    
    private void Update () 
    {
        if (_target == null) return;

        if (Math.Abs(_agent.velocity.magnitude) < float.Epsilon ) FaceTarget();

        _agent.SetDestination(_target.position);
    }
        
    private void FaceTarget () 
    {
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation( new Vector3(direction.x,0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime * 5f);
    }    
    
    public void MoveToPoint(Vector3 point) =>_agent.SetDestination(point);    
       
    public void FollowTarget (Interactable newTarget) 
    {
        _agent.stoppingDistance = newTarget.radius;
        _target = newTarget.interactionCenter;
    }

    public void StopFollowingTarget () 
    {
        _agent.stoppingDistance = 0f;
        _agent.ResetPath();
        _target = null;
    }
}
