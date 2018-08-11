using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private const bool ControllerDebug = false;
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                instance = gameObject.AddComponent<InputManager>();
                gameObject.name = "InputManager";
            }

            return instance;
        }
    }
    
    public const float JoystickTolerance = 0.9f;
    
    public static List<IControllerInput> controllers = new List<IControllerInput>();
    public static Entity ActiveEntity = Entity.MainCharacter;

    public enum Entity
    {
        MainCharacter
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        RefreshAndLoadControllers();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (ControllerDebug)
        {
            Debug.Log("Left: " + CheckLeftInputIsDown());
            Debug.Log("Right: " + CheckRightInputIsDown());
            Debug.Log("Up: " + CheckUpInputIsDown());
            Debug.Log("Down: " + CheckDownInputIsDown());
            Debug.Log("Triangle: " + CheckTriangleInputPress());
            Debug.Log("Circle: " + CheckCircleInputPress());
            Debug.Log("X: " + CheckXInputPress());
            Debug.Log("Square: " + CheckSquareInputPress());
        }
    }

    public void RefreshAndLoadControllers()
    {
        controllers.Clear();
        controllers.Add(new InputKeyboard());
        controllers.Add(InputGamepad.LoadConfiguration());
    }

    public InputGamepad GetGamepadController()
    {
        // Assuming the gamepad will be found at index 1
        return (InputGamepad)controllers[1];
    }
    
    public bool CheckActionInputPress()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckActionInputPress();
        }
        return isPressed;
    }
    
    public bool CheckCastInputPress()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckCastInputPress();
        }
        return isPressed;
    }
    
    public bool CheckCastInputIsDown()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckCastInputIsDown();
        }
        return isPressed;
    }
    
    public bool CheckCastInputRelease()
    {
        var isReleased = false;
        foreach (IControllerInput controller in controllers)
        {
            isReleased = isReleased || controller.CheckCastInputRelease();
        }
        return isReleased;
    }  
    
    public bool CheckSneakInput()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckSneakInput();
        }
        return isPressed;
    }
    
    public float GetHorizontalAxisRaw()
    {
        foreach (IControllerInput controller in controllers)
        {
            var horizontalAxisValue = controller.GetHorizontalAxisRaw();
            if (Math.Abs(horizontalAxisValue) > JoystickTolerance)
            {
                return horizontalAxisValue;
            } 
        }
        return 0f;
    }
    
    public float GetVerticalAxisRaw()
    {
        foreach (IControllerInput controller in controllers)
        {
            var verticalAxisValue = controller.GetVerticalAxisRaw();
            if (Math.Abs(verticalAxisValue) > JoystickTolerance)
            {
                return verticalAxisValue;
            } 
        }
        return 0f;
    }
    
    
    
    // GENERIC INPUTS
   
    public bool CheckDownInputIsDown()
    {
        var isDown = false;
        foreach (IControllerInput controller in controllers)
        {
            isDown = isDown || controller.CheckDownInputIsDown();
        }
        return isDown;
    }   
    public bool CheckLeftInputIsDown()
    {
        var isDown = false;
        foreach (IControllerInput controller in controllers)
        {
            isDown = isDown || controller.CheckLeftInputIsDown();
        }
        return isDown;
    }   
    public bool CheckRightInputIsDown()
    {
        var isDown = false;
        foreach (IControllerInput controller in controllers)
        {
            isDown = isDown || controller.CheckRightInputIsDown();
        }
        return isDown;
    }   
    public bool CheckUpInputIsDown()
    {
        var isDown = false;
        foreach (IControllerInput controller in controllers)
        {
            isDown = isDown || controller.CheckUpInputIsDown();
        }
        return isDown;
    }
    
    public bool CheckCircleInputPress()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckCircleInputPress();
        }
        return isPressed;
    }
    public bool CheckSquareInputPress()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckSquareInputPress();
        }
        return isPressed;
    }
    public bool CheckTriangleInputPress()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckTriangleInputPress();
        }
        return isPressed;
    }
    public bool CheckXInputPress()
    {
        var isPressed = false;
        foreach (IControllerInput controller in controllers)
        {
            isPressed = isPressed || controller.CheckXInputPress();
        }
        return isPressed;
    }
    
}