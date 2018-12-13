using UnityEngine;

public class CameraController : MonoBehaviour
{    
    private Transform _transform;

    [SerializeField, Tooltip("отступ камеры от игрока")]
    private Vector3 _offset;
    
    [SerializeField, Tooltip("высота точки слежения (чтобы камера смотрела персонажу выше пояса)")]
    private float _pitch = 2f;

    [SerializeField, Tooltip("регулировка чувствительности колесика мыши")]
    private float _zoomSpeed = 4f;

    /// TODO: собрать в струкуру Vector2 zoomClamp
    [SerializeField, Tooltip("ограничение приближения камеры")]
    private float _minZoom = 5f;    
    [SerializeField, Tooltip("ограничение удаления камеры")]
    private float _maxZoom = 15f;

    [SerializeField, Tooltip("текущее значение приближения")]    
    private float _currentZoom = 10f;
    
    private float _currentRot = 0f; /// текущий угол вращения камеры    
    private float _prevMouseX; /// предыдущее положение мыши для отслеживания ее перемещения за кадр

    [SerializeField, Tooltip("целевой объект слежения камеры")]
    private Transform _target;        

    public Transform Target { set => _target = value; }

    void Awake()
    {
        _transform = transform;
    }
    void Update()
    {
        if (_target == null) return;            

        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed; /// изменение зума        
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);/// ограничение зума

        if (Input.GetMouseButton(2))            
            _currentRot += Input.mousePosition.x - _prevMouseX; /// изменение угла поворота камеры
        
        _prevMouseX = Input.mousePosition.x; /// обновляем предыдущее положение мыши

    }
    void LateUpdate()
    {
        if (_target == null) return;
        
        _transform.position = _target.position - _offset*_currentZoom;            
        _transform.LookAt(_target.position + Vector3.up * _pitch); /// поворот камеры на игрока                               
        _transform.RotateAround(_target.position, Vector3.up, _currentRot);/// применение дополнительного вращения
    }

}
