using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class PlayerController : MonoBehaviour
{    
    [SerializeField, Tooltip("слой взаимодействия перемещения")]
    private LayerMask _movementMask;

    private Camera _cam;
    private UnitMovement _unitMovement;

    void Awake()
    {
        _cam = Camera.main;
        _unitMovement = GetComponent<UnitMovement>();

        _cam.GetComponent<CameraController>().Target = transform; /// установка цели слежения для камеры
    }

    void Update()
    {       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, _movementMask))
                _unitMovement.MoveToPoint(hit.point);
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100f, _movementMask))
        //    { }
        //}
    }
}
