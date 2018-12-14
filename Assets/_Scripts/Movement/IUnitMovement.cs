using UnityEngine;

public interface IUnitMovement 
{
    void Init(IPlayerInput playerInput, PlayerController playerCtrl);
    void Tick();
}
