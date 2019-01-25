using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class NetPlayerController : NetworkBehaviour
{
    [SerializeField] private LayerMask _movementMask;

    private Character _character;
    private Camera _cam;

    private bool _hasCharacter = false;

    private void Awake() => _cam = Camera.main;

    private void Update()
    {
        if (!isLocalPlayer || !_hasCharacter) return;

        if (Input.GetMouseButtonDown(1) && Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition),
                out var moveHit, 100f, _movementMask))
            CmdSetMovePoint(moveHit.point);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition),
                out var interactHit, 100f, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            GbInteractable interactable = interactHit.collider.GetComponent<GbInteractable>();
            if (interactable != null)
                CmdSetFocus(interactable.GetComponent<NetworkIdentity>());
        }
    }

    private void OnDestroy()
    {
        if (_character != null) Destroy(_character.gameObject);
    }

    public void SetCharacter(Character character, bool IsLocalPlayer)
    {
        _character = character;
        _hasCharacter = true;
        if (IsLocalPlayer)
            _cam.GetComponent<CameraController>().Target = character.transform;
    }

    [Command]
    private void CmdSetMovePoint(Vector3 point) => _character.SetMovePoint(point);

    [Command]
    public void CmdSetFocus(NetworkIdentity newFocus) =>
        _character.SetNewFocus(newFocus.GetComponent<GbInteractable>());
}