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

//        public override bool OnCheckObserver(NetworkConnection connection) => false;

        public override void OnStartAuthority()
        {
            if (isServer)
            {
                Character character =  CreateCharacter();
                _controller.SetCharacter(character, true);
                InventoryUI.instance.SetInventory(character.Inventory);
            }
            else CmdCreatePlayer();
        }
        
        [Command]
        private void CmdCreatePlayer() =>  _controller.SetCharacter(CreateCharacter(), true);

        private Character CreateCharacter()
        {
            GameObject unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
            var character = unit.GetComponent<Character>();
            
            NetworkServer.Spawn(unit);
            _unitIdentity = unit.GetComponent<NetworkIdentity>();                       
            character.SetInventory(GetComponent<GbInventory>());            

            return character;
        }
  
        [ClientCallback]
        private void HookUnitIdentity(NetworkIdentity unit)
        {
            if (!isLocalPlayer) return;

            _unitIdentity = unit;
            Character character = unit.GetComponent<Character>();

            _controller.SetCharacter(character, true);
            character.SetInventory(GetComponent<GbInventory>());
            InventoryUI.instance.SetInventory(character.Inventory);
        }
    }
}