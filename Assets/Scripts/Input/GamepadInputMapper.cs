using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class GamepadInputMapper : MonoBehaviour
{
    
    private bool waitingForAxisToZeroOut;
    
    private Dictionary<string, string> _controllerInputDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> GetInputDictionary()
    {
        return _controllerInputDictionary;
    }
    
    private ArrayList _inputButtonNameList = new ArrayList();
    public const string Action = "action";
    public const string Cast = "cast";
    public const string Sneak = "sneak";
    public const string Triangle = "triangle";
    public const string Circle = "circle";
    public const string X = "x";
    public const string Square = "square";

    private ArrayList _inputAxesNameList = new ArrayList();
    public const string HorizontalAxis = "horizontal";
    public const string VerticalAxis = "vertical";

    public string _pendingInputBindName;
    public string GetPendingInputBindName()
    {
        return _pendingInputBindName;
    }

    private void Awake()
    {
        ControllerConfigurationFilePath = Path.Combine(Application.persistentDataPath, ControllerConfigurationFileName);
        
        //Build list of buttons that need to be bound
        _inputButtonNameList.Add(Action);
        _inputButtonNameList.Add(Cast);
        _inputButtonNameList.Add(Sneak);
        _inputButtonNameList.Add(Triangle);
        _inputButtonNameList.Add(Circle);
        _inputButtonNameList.Add(X);
        _inputButtonNameList.Add(Square);

        //Build list of axes that need to be bound
        _inputAxesNameList.Add(HorizontalAxis);
        _inputAxesNameList.Add(VerticalAxis);
        
        DebugConsole.WriteLog("Listening for the new controller mapping");
    }

    // Since we can't do a hard loop to listen for input, we have to be clever about how we keep 
    // track of where we are in the input binding process. Every frame, we figure out where in the mapping 
    // process we are, and then listen for the next input from the player
    void Update()
    {
        if (isControllerMappingCompete())
        {
            EndMapping();
        }
        
        if (!areAllButtonsBound())
        {
            listenForNextButtonBind();
        } 
        else if (!areAllAxesBound())
        {
            if (waitingForAxisToZeroOut && !areAllAxesZero())
            {
                return;
            }
            
            listenForNextDirectionBind();
        }
    }

    bool isControllerMappingCompete()
    {
        return areAllButtonsBound() && areAllAxesBound();
    }
    
    bool areAllButtonsBound()
    {
        foreach (string actionName in _inputButtonNameList)
        {
            if (!_controllerInputDictionary.ContainsKey(actionName))
            {
                return false;
            }
        }
        return true;
    }

    bool areAllAxesBound()
    {
        foreach (string axisName in _inputAxesNameList)
        {
            if (!_controllerInputDictionary.ContainsKey(axisName))
            {
                return false;
            }
        }
        return true;
    }
    
    void listenForNextButtonBind()
    {
        
        for (int i=0; i<_inputButtonNameList.Count; i++)
        {
            string inputType = (String) _inputButtonNameList[i];
            
            if (!_controllerInputDictionary.ContainsKey(inputType))
            {
                _pendingInputBindName = inputType;
                
                for (int joystickIndex = 1; joystickIndex < 10; joystickIndex++)
                {
                    for (int buttonIndex = 0; buttonIndex < 20; buttonIndex++)
                    {
                        if (Input.GetKeyDown("joystick "+ joystickIndex +" button " + buttonIndex))
                        {
                            var input = String.Format("joystick {0} button {1}", joystickIndex, buttonIndex);
                            _controllerInputDictionary[inputType] = input;
                            waitingForAxisToZeroOut = true;
                        }
                    }
                }
                break;
            }
        }
    }

    bool areAllAxesZero()
    {
        for (int axisIndex = 1; axisIndex < 16; axisIndex++)
        {
            var axis = String.Format("Axis_{0}", axisIndex);
            if (Mathf.Abs(Input.GetAxisRaw(axis)) > 0.2F)
            {
                return false;
            }
        }
        waitingForAxisToZeroOut = false;
        return true;
    }
    
    void listenForNextDirectionBind()
    {
        
        for (int i=0; i<_inputAxesNameList.Count; i++)
        {
            string inputType = (String) _inputAxesNameList[i];
            
            if (!_controllerInputDictionary.ContainsKey(inputType))
            {
                _pendingInputBindName = inputType;
                
                for (int axisIndex = 1; axisIndex < 16; axisIndex++)
                {
                    var axis = String.Format("Axis_{0}", axisIndex);
                    if (Mathf.Abs(Input.GetAxisRaw(axis)) > 0.2F)
                    {
                        _controllerInputDictionary[inputType] = axis;
                        waitingForAxisToZeroOut = true;
                    }
                }
                break;
            }
        }
    }
    
    void EndMapping()
    {
        Debug.Log("Finished reading joypad input. Saving configuration.");
        InputGamepad gamepad = new InputGamepad(_controllerInputDictionary);
        gamepad.SaveConfiguration();
        InputManager.Instance.RefreshAndLoadControllers();
        Destroy(gameObject);
    }
}