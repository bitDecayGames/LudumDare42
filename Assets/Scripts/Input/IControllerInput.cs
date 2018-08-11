
public interface IControllerInput
{
    bool CheckActionInputPress();
    bool CheckCastInputPress();
    bool CheckCastInputIsDown();
    bool CheckCastInputRelease();
    bool CheckSneakInput();
    float GetHorizontalAxisRaw();
    float GetVerticalAxisRaw();
    bool CheckDownInputIsDown();
    bool CheckLeftInputIsDown();
    bool CheckUpInputIsDown();
    bool CheckRightInputIsDown();
    bool CheckCircleInputPress();
    bool CheckSquareInputPress();
    bool CheckTriangleInputPress();
    bool CheckXInputPress();
}
