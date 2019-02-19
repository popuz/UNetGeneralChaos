﻿using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Serialization;

namespace UNetGeneralChaos
{
    public class PlayerLoader : NetworkBehaviour
    {
        [SerializeField] private GameObject unitPrefab;

        [SerializeField] private NetPlayerController controller;
        [SerializeField] Player player;

        public override void OnStartAuthority()
        {
            CmdCreatePlayer();
        }


        [Command]
        public void CmdCreatePlayer()
        {
            Character character = CreateCharacter();
            player.Setup(character, GetComponent<Inventory>(), GetComponent<Equipment>(), isLocalPlayer);
            controller.SetCharacter(character, isLocalPlayer);
        }

        public Character CreateCharacter()
        {
            UserAccount acc = AccountManager.GetAccount(connectionToClient);

            GameObject unit = Instantiate(unitPrefab, acc.data.posCharacter, Quaternion.identity);
            NetworkServer.Spawn(unit);
            TargetLinkCharacter(connectionToClient, unit.GetComponent<NetworkIdentity>());
            return unit.GetComponent<Character>();
        }


        [TargetRpc]
        void TargetLinkCharacter(NetworkConnection target, NetworkIdentity unit)
        {
            Character character = unit.GetComponent<Character>();
            player.Setup(character, GetComponent<Inventory>(), GetComponent<Equipment>(), true);
            controller.SetCharacter(character, true);
        }

        private void OnDestroy()
        {
            if (isServer && player.character != null)
            {
                UserAccount acc = AccountManager.GetAccount(connectionToClient);
                acc.data.posCharacter = player.character.transform.position;
                Destroy(player.character.gameObject);
                NetworkManager.singleton.StartCoroutine(acc.Quit());
            }
        }
    }
}