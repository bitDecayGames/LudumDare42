using UnityEngine;

public partial class CameraController
{
    private const float defaultZoomSpeed = .02f;
    
    private float _zoomSpeed = defaultZoomSpeed;
    
    public static float ZoomSpeedHoldUpItem = .15f;
    
    public void ResetZoom()
    {
        _goalZoom = DefaultZoom;
    }
    
    public void SetZoom(float goalZoomValue)
    {
        _goalZoom = goalZoomValue;
        _zoomSpeed = defaultZoomSpeed;
    }
    
    public void SetZoom(float goalZoomValue, float zoomSpeed)
    {
        _goalZoom = goalZoomValue;
        _zoomSpeed = zoomSpeed;
    }

    private void MoveCameraZoom()
    {
        int direction;
        if (_mainCamera.orthographicSize - _goalZoom >= 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        _mainCamera.orthographicSize += _zoomSpeed * direction;
        if (Mathf.Abs(_mainCamera.orthographicSize - _goalZoom) <= _zoomSpeed)
        {
            _mainCamera.orthographicSize = _goalZoom;
        }
    }
}