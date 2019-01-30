using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Serialization;

namespace UNetGeneralChaos
{
    public class PlayerLoader : NetworkBehaviour
    {
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private NetPlayerController _controller;
        [SerializeField] Player player;

        [SyncVar(hook = nameof(HookUnitIdentity))]
        private NetworkIdentity _unitIdentity;

//        public override bool OnCheckObserver(NetworkConnection connection) => false;

        public override void OnStartAuthority()
        {
            if (isServer)
            {
                Character character = CreateCharacter();
                player.Setup(character, GetComponent<Inventory>(), GetComponent<Equipment>(), true);
                _controller.SetCharacter(character, true);
            }
            else CmdCreatePlayer();
        }

        [Command]
        public void CmdCreatePlayer()
        {
            Character character = CreateCharacter();
            player.Setup(character, GetComponent<Inventory>(), GetComponent<Equipment>(), false);
            _controller.SetCharacter(character, false);
        }

        public Character CreateCharacter()
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(unit);
            _unitIdentity = unit.GetComponent<NetworkIdentity>();
            return unit.GetComponent<Character>();
        }

        [ClientCallback]
        void HookUnitIdentity(NetworkIdentity unit)
        {
            if (isLocalPlayer)
            {
                _unitIdentity = unit;
                Character character = unit.GetComponent<Character>();
                player.Setup(character, GetComponent<Inventory>(), GetComponent<Equipment>(), true);
                _controller.SetCharacter(character, true);
            }
        }
    }
}