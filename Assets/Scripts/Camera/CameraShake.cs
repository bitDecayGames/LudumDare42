using UnityEngine;

public partial class CameraController
{
    private bool _isShaking;
    private float _shakeDuration;
    private float _shakeIntensity;

    private Vector3 _preShakePosition;

    /// <summary>
    /// Basic camera shake call
    /// </summary>
    public void InitiateScreenShake()
    {
        _shakeDuration = .1f;
        _shakeIntensity = .1f;
        _isShaking = true;
        _preShakePosition = _mainCamera.transform.localPosition;
      
    }
   
    public void InitiateScreenShake(float duration, float intensity)
    {
        _shakeDuration = duration;
        _shakeIntensity = intensity;
        _isShaking = true;
        _preShakePosition = _mainCamera.transform.localPosition;
    }

    private void ShakeScreen()
    {
        if (_shakeDuration > 0)
        {
            _mainCamera.transform.localPosition = _preShakePosition + Random.insideUnitSphere * _shakeIntensity;

            _shakeDuration -= Time.deltaTime;
        }
        else
        {
            _isShaking = false;
            _mainCamera.transform.localPosition = _preShakePosition;
        }
    }
}