
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputGamepad : IControllerInput
{
    
    private const string defaultXBox360Mapping =
            "{\"Action\":\"joystick 1 button 0\",\"Cast\":\"joystick 1 button 2\",\"Sneak\":\"joystick 1 button 5\",\"Circle\":\"joystick 1 button 1\",\"Square\":\"joystick 1 button 2\",\"Triangle\":\"joystick 1 button 3\",\"X\":\"joystick 1 button 0\",\"HorizontalAxis\":\"Axis_6\",\"VerticalAxis\":\"Axis_7\"}\r\n";
    
    public string Action;
    public string Cast;
    public string Sneak;
    public string Circle;
    public string Square;
    public string Triangle;
    public string X;

    public string HorizontalAxis;
    public string VerticalAxis;
    
    
    public InputGamepad(Dictionary<string, string> inputDictionary)
    {
        Action = inputDictionary[GamepadInputMapper.Action];
        Cast = inputDictionary[GamepadInputMapper.Cast];
        Sneak = inputDictionary[GamepadInputMapper.Sneak];
        Circle = inputDictionary[GamepadInputMapper.Circle];
        Square = inputDictionary[GamepadInputMapper.Square];
        Triangle = inputDictionary[GamepadInputMapper.Triangle];
        X = inputDictionary[GamepadInputMapper.X];
        HorizontalAxis = inputDictionary[GamepadInputMapper.HorizontalAxis];
        VerticalAxis = inputDictionary[GamepadInputMapper.VerticalAxis];
    }

    public void SaveConfiguration()
    {
        StreamWriter writer = new StreamWriter(GamepadInputMapper.ControllerConfigurationFilePath);
        var saveDataAsJson = JsonUtility.ToJson(this);
        writer.WriteLine(saveDataAsJson);
        writer.Close();
    }

    public static void GenerateAndSaveDefaultControllerMapping()
    {
        StreamWriter writer = new StreamWriter(GamepadInputMapper.ControllerConfigurationFilePath);
        writer.WriteLine(defaultXBox360Mapping);
        writer.Close();
    }

    public static InputGamepad LoadConfiguration()
    {
        if (!File.Exists(GamepadInputMapper.ControllerConfigurationFilePath))
        {
            return JsonUtility.FromJson<InputGamepad>(defaultXBox360Mapping);
        }
        
        StreamReader reader = new StreamReader(GamepadInputMapper.ControllerConfigurationFilePath);
        var saveDataJson = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<InputGamepad>(saveDataJson);
    }

    public bool CheckActionInputPress()
    {
        return Input.GetKeyDown(Action);
    }

    public bool CheckCastInputPress()
    {
        return Input.GetKeyDown(Cast);
    }

    public bool CheckCastInputIsDown()
    {
        return Input.GetKey(Cast);
    }

    public bool CheckCastInputRelease()
    {
        return Input.GetKeyUp(Cast);
    }

    public bool CheckSneakInput()
    {
        return Input.GetKey(Sneak);
    }

    public float GetHorizontalAxisRaw()
    {
        if (HorizontalAxis == null || HorizontalAxis.Equals(""))
        {
            return 0;
        }
        return Input.GetAxisRaw(HorizontalAxis);
    }

    public float GetVerticalAxisRaw()
    {
        if (VerticalAxis == null || VerticalAxis.Equals(""))
        {
            return 0;
        }
        return Input.GetAxisRaw(VerticalAxis);
    }

    public bool CheckDownInputIsDown()
    {
        var dpadInput = Input.GetAxisRaw(VerticalAxis);

        if (dpadInput < 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckLeftInputIsDown()
    {
        var dpadInput = Input.GetAxisRaw(HorizontalAxis);

        if (dpadInput < 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckUpInputIsDown()
    {
        var dpadInput = Input.GetAxisRaw(VerticalAxis);

        if (dpadInput > 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckRightInputIsDown()
    {
        var dpadInput = Input.GetAxisRaw(HorizontalAxis);

        if (dpadInput > 0)
        {
            return true;
        }
        return false;
    }

    public bool CheckCircleInputPress()
    {
        return Input.GetKeyDown(Circle);
    }

    public bool CheckSquareInputPress()
    {
        return Input.GetKeyDown(Square);
    }

    public bool CheckTriangleInputPress()
    {
        return Input.GetKeyDown(Triangle);
    }

    public bool CheckXInputPress()
    {
        return Input.GetKeyDown(X);
    }
}