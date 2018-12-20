using System;
using UnityEngine;

namespace UNetGeneralChaos
{
    public class ClickToMoveInputScheme : IPlayerInput
    {
        public event Action FireOnce;

        private bool _fireIsReleased = false;
        public bool IsFiring { get; private set; }

        public Vector3 CursorPosition => Input.mousePosition;
        public float Horizontal => 0f;
        public float Vertical => 0f;

        public void ReadInput()
        {
            IsFiring = Input.GetAxisRaw("Fire1") != 0;
            HandleActionOnce(IsFiring, FireOnce, ref _fireIsReleased);
        }

        private void HandleActionOnce(bool isActingFlag, Action ActionOnFirstAct, ref bool releaseFlag)
        {
            if (releaseFlag == isActingFlag)
            {
                releaseFlag = !releaseFlag;

                if (isActingFlag)
                    ActionOnFirstAct?.Invoke();
            }
        }
    }
}
