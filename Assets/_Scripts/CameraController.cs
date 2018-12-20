using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("отступ камеры от игрока")]
    private Vector3 _offset;    
    [SerializeField, Tooltip("регулировка чувствительности колесика мыши")]
    private float _zoomSpeed = 4f;
    [SerializeField, Tooltip("ограничение приближения камеры")]
    private Vector2 _zoomClamp = new Vector2(0.3f, 2.2f); /// ограничение удаления камеры
    [SerializeField, Tooltip("высота точки слежения (чтобы камера смотрела персонажу выше пояса)")]
    private float _pitch = 2f;

    private Transform _transform;
    private float _currentRot = 0f; /// текущий угол вращения камеры  
    private float _currentZoom = 10f; /// текущее значение приближения
    private float _prevMouseX; /// предыдущее положение мыши для отслеживания ее перемещения за кадр

    public Transform Target { get; set; }

    void Awake() => _transform = transform;    

    void Update()
    {
        if (Target == null) return;            

        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed; /// изменение зума        
        _currentZoom = Mathf.Clamp(_currentZoom, _zoomClamp.x, _zoomClamp.y);/// ограничение зума

        if (Input.GetMouseButton(2))            
            _currentRot += Input.mousePosition.x - _prevMouseX; /// изменение угла поворота камеры
        
        _prevMouseX = Input.mousePosition.x; /// обновляем предыдущее положение мыши
    }

    void LateUpdate()
    {
        if (Target == null) return;        
        _transform.position = Target.position - _offset*_currentZoom;            
        _transform.LookAt(Target.position + Vector3.up * _pitch); /// поворот камеры на игрока                               
        _transform.RotateAround(Target.position, Vector3.up, _currentRot);/// применение дополнительного вращения
    }

}
