using UnityEngine;
using UnityEngine.Networking;

namespace UNetGeneralChaos
{
    public class NetPlayerSetup : NetworkBehaviour
    {
        [SerializeField] MonoBehaviour[] _disableBehaviours;

        void Awake()
        {
            if (hasAuthority) return;
            SetBehavioursToDisableForOthers(false);
        }

        public override void OnStartAuthority() => SetBehavioursToDisableForOthers(true);

        private void SetBehavioursToDisableForOthers(bool flag)
        {
            for (int i = 0; i < _disableBehaviours.Length; i++)
                _disableBehaviours[i].enabled = flag;
        }
    }
}
