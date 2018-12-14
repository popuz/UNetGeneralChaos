using System;
using UnityEngine;

public class ClickToMoveInputScheme : IPlayerInput
{
    public event Action FireOnce;

    public Vector3 CursorPosition => Input.mousePosition;
    public bool IsFiring { get; private set; }

    private bool _fireIsReleased = false;

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
                ActionOnFirstAct.Invoke();
        }
    }
}
