using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Serialization;

namespace UNetGeneralChaos
{
    public class PlayerLoader : NetworkBehaviour
    {
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private NetPlayerController _controller;

        [SyncVar(hook = nameof(HookUnitIdentity))]
        private NetworkIdentity _unitIdentity;

//        public override void OnStartServer() => 
//            NetworkServer.SpawnWithClientAuthority( Instantiate(_unitPrefab, transform.position, Quaternion.identity), 
//                                                    this.gameObject);

        public override bool OnCheckObserver(NetworkConnection connection) => false;

        public override void OnStartAuthority()
        {
            if (isServer)
            {
                Character character = SpawnPlayer(true);
                _controller.SetCharacter(character, true);
                InventoryUI.instance.SetInventory(character.Inventory);
            }
            else CmdCreatePlayer();
        }

        [Command]
        private void CmdCreatePlayer() => SpawnPlayer(false);

        private Character SpawnPlayer(bool IsLocalPlayer)
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(unit);
            _unitIdentity = unit.GetComponent<NetworkIdentity>();

            var character = unit.GetComponent<Character>();
            character.SetInventory(GetComponent<GbInventory>());
            _controller.SetCharacter(character, IsLocalPlayer);

            return character;
        }

        [ClientCallback]
        private void HookUnitIdentity(NetworkIdentity unit)
        {
            if (!isLocalPlayer) return;

            _unitIdentity = unit;
            Character character = unit.GetComponent<Character>();
            
            character.SetInventory(GetComponent<GbInventory>());
            _controller.SetCharacter(character, true);                       
            InventoryUI.instance.SetInventory(character.Inventory);
        }
    }
}