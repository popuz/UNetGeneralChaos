using UnityEngine.Networking;
using UnityEngine;

public class UnitLoader : NetworkBehaviour
{
    [SerializeField]
    private GameObject _unitPfb;

    public override void OnStartServer() => NetworkServer.SpawnWithClientAuthority( Instantiate(_unitPfb), this.gameObject );    
}
