using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{    
    private int _maxHealth = 100;

    [SyncVar]
    private int _curHealth;

    public override void OnStartAuthority() => _curHealth = _maxHealth;
}
