using System;
using UnityEngine;

namespace UNetGeneralChaos
{
    public class WASDInputScheme : IPlayerInput
    {
        public event Action FireOnce;

        private bool _fireIsReleased = false;
        public bool IsFiring => false;
        public Vector3 CursorPosition => Vector3.zero;

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public void ReadInput()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
        }
    }
}
