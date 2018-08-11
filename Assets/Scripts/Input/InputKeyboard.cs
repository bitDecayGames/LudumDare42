
using System;
using UnityEngine;

public class InputKeyboard : IControllerInput
{
    private const String HorizontalAxis = "Horizontal";
    private const String VerticalAxis = "Vertical";

    public bool CheckActionInputPress()
    {
        return Input.GetKeyDown(KeyCode.X);
    }

    public bool CheckCastInputPress()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }

    public bool CheckCastInputIsDown()
    {
        return Input.GetKey(KeyCode.Z);
    }

    public bool CheckCastInputRelease()
    {
        return Input.GetKeyUp(KeyCode.Z);
    }

    public bool CheckSneakInput()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public float GetHorizontalAxisRaw()
    {
        
        return Input.GetAxisRaw(HorizontalAxis);
    }

    public float GetVerticalAxisRaw()
    {
        return Input.GetAxisRaw(VerticalAxis);
    }

    public bool CheckDownInputIsDown()
    {
        var keyboardInput = Input.GetAxisRaw(VerticalAxis);

        if (keyboardInput < 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckLeftInputIsDown()
    {
        var keyboardInput = Input.GetAxisRaw(HorizontalAxis);

        if (keyboardInput < 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckUpInputIsDown()
    {
        var keyboardInput = Input.GetAxisRaw(VerticalAxis);

        if (keyboardInput > 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckRightInputIsDown()
    {
        var keyboardInput = Input.GetAxisRaw(HorizontalAxis);

        if (keyboardInput > 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckCircleInputPress()
    {
        return Input.GetKeyDown(KeyCode.D);
    }

    public bool CheckSquareInputPress()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    public bool CheckTriangleInputPress()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    public bool CheckXInputPress()
    {
        return Input.GetKeyDown(KeyCode.S);
    }
}
