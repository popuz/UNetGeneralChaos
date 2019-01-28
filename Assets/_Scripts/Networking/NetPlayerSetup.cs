using UnityEngine;
using UnityEngine.Networking;

namespace UNetGeneralChaos
{
    public class NetPlayerSetup : NetworkBehaviour
    {
        [SerializeField] MonoBehaviour[] _disableBehaviours;

        private void Awake()
        {
            if (hasAuthority) return;
            SetBehavioursToDisableForOthers(false);
        }

        public override void OnStartAuthority() => SetBehavioursToDisableForOthers(true);

        private void SetBehavioursToDisableForOthers(bool flag)
        {
            for (int i = 0; i < _disableBehaviours.Length; i++)
                if (_disableBehaviours[i] != null)  _disableBehaviours[i].enabled = flag;
        }
    }
}
