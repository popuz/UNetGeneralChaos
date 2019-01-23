using UnityEngine;
using UnityEngine.AI;

namespace UNetGeneralChaos
{
    /// <summary>
    /// Sync animation with NavMeshAgent state.
    /// </summary>
    public class UnitAnimation
    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        
        private static readonly int MoveTriggerHash = Animator.StringToHash("Move");

        public UnitAnimation(Animator animator, NavMeshAgent agent)
        {
            _animator = animator;
            _agent = agent;
        }

        /// <summary>
        /// Update/FixedUpdate analogue: called in every frame to perform the synchronization of animation with logic.
        /// </summary>
        public void Tick() => _animator.SetBool(MoveTriggerHash, _agent.hasPath);
    }
}

