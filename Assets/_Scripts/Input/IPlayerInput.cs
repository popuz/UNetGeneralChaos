using System;
using UnityEngine;

namespace UNetGeneralChaos
{
    public interface IPlayerInput
    {
        void ReadInput();

        /// Events        
        event Action FireOnce;

        /// Properties 
        bool IsFiring { get; } // 0, 1  

        float Horizontal { get; }
        float Vertical { get; }
        Vector3 CursorPosition { get; }
    }
}
