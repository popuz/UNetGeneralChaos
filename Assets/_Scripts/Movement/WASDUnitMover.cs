using UnityEngine;

public class WASDUnitMover : IUnitMovement
{
    private IPlayerInput _input;
    public bool UseTick => true;

    private Transform _playerTransform;
    public float Speed { get; set; }    

    public WASDUnitMover(Transform playerTransform, float speed)
    {
        _playerTransform = playerTransform;
        Speed = speed;
    }

    public void Init(IPlayerInput playerInput)
    {
        Debug.Log($"Using {nameof(WASDUnitMover)} for player Movement");
        _input = playerInput;        
    }

    public void Tick() => HandleMovement(Time.fixedDeltaTime);  

    public void Tick(float fixedDeltaTime) => HandleMovement(fixedDeltaTime);


    private void HandleMovement(float fixedDeltaTime)
    {
        _playerTransform.position += new Vector3(_input.Horizontal * Speed * fixedDeltaTime,
                                                 0f,
                                                 _input.Vertical * Speed * fixedDeltaTime);
    }
    
}
