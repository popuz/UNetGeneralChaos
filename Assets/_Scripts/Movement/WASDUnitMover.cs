using UnityEngine;

public class WASDUnitMover : IUnitMovement
{
    private IPlayerInput _input;

    private Transform _playerTransform;
    private float _speed;

    public WASDUnitMover(Transform playerTransform, float speed)
    {
        _playerTransform = playerTransform;
        _speed = speed;
    }

    public void Init(IPlayerInput playerInput)
    {
        Debug.Log($"Using {nameof(WASDUnitMover)}");
        _input = playerInput;        
    }

    public void Tick()
    {        
        _playerTransform.position += new Vector3(_input.Horizontal * _speed * Time.fixedDeltaTime, 0f, _input.Vertical * _speed * Time.fixedDeltaTime);
    }
}
