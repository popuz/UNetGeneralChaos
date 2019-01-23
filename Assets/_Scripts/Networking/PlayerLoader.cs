using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Serialization;

namespace UNetGeneralChaos
{
    public class PlayerLoader : NetworkBehaviour
    {
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private NetPlayerController _controller;
        [SyncVar(hook = nameof(HookUnitIdentity))] private NetworkIdentity _unitIdentity;

//        public override void OnStartServer() => 
//            NetworkServer.SpawnWithClientAuthority( Instantiate(_unitPrefab, transform.position, Quaternion.identity), 
//                                                    this.gameObject);

        public override void OnStartAuthority()
        {
            if (isServer)
                SpawnPlayer(true);
            else
                CmdCreatePlayer();
        }

        [Command]
        private void CmdCreatePlayer() => SpawnPlayer(false);

        private void SpawnPlayer(bool IsLocalPlayer)
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(unit);
            _unitIdentity = unit.GetComponent<NetworkIdentity>();
            _controller.SetCharacter(unit.GetComponent<Character>(), IsLocalPlayer);
        }

        [ClientCallback]
        private void HookUnitIdentity(NetworkIdentity unit)
        {
            if (!isLocalPlayer) return;

            _unitIdentity = unit;
            _controller.SetCharacter(unit.GetComponent<Character>(), true);
        }
    }
}