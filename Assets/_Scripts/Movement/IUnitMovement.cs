using UnityEngine;

public interface IUnitMovement 
{
    void Init(IPlayerInput playerInput);
    void Tick();
}
