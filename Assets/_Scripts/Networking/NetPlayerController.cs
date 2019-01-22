using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class NetPlayerController : NetworkBehaviour
{
    [SerializeField ] private LayerMask _movementMask;
    
    private Character _character;
    private Camera _cam;

    private bool _hasCharacter = false;
    
    private void Awake () => _cam = Camera.main;    
    
    private void Update ()
    {
        if (isLocalPlayer && _hasCharacter && Input.GetMouseButtonDown(1)
            && Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out var hit, 100f, _movementMask))
        
            CmdSetMovePoint(hit.point);        
    }
    
    private void OnDestroy () 
    {
        if (_character != null ) Destroy(_character.gameObject);
    }
    
    public void SetCharacter (Character character, bool isLocalPlayer) 
    {
        _character = character;
        _hasCharacter = true;
        if (isLocalPlayer)
            _cam.GetComponent<CameraController>().Target = character.transform;
    }
    
        [Command]
        private void CmdSetMovePoint (Vector3 point) => _character.SetMovePoint(point);                           
}
