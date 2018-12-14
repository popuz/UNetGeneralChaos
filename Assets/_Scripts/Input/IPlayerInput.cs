using System;
using UnityEngine;

public interface IPlayerInput
{
    void ReadInput();

    /// Events        
    event Action FireOnce;

    /// Properties 
    Vector3 CursorPosition { get; }
    bool IsFiring { get; } // 0, 1  
}
