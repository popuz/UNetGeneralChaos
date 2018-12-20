using UnityEngine;
using UnityEngine.AI;

namespace UNetGeneralChaos
{
    /// <summary>
    /// Sync animation with NavMeshAgent state.
    /// </summary>
    public class UnitAnimation
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        
        private readonly string IsMovingTriggerName = "IsMoving";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animator">Targeted AnimatorComponent to be under control</param>
        /// <param name="agent">NavMeshAgentComponent that should be taken</param>
        public UnitAnimation(Animator animator, NavMeshAgent agent)
        {
            _animator = animator;
            _agent = agent;
        }

        /// <summary>
        /// Update/FixedUpdate analouge: called in every frame to perform the syncronization of animation with logic.
        /// </summary>
        public void Tick() => _animator.SetBool(IsMovingTriggerName, _agent.hasPath);
    }
}

