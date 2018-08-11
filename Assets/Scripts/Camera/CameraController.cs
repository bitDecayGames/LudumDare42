using UnityEngine;

public partial class CameraController : MonoBehaviour
{
    public Transform FollowTransform;
    
    private const float SmoothTime = .5f;
    private const float DefaultZoom = 6f;

    private float _goalZoom = 6f;
    
    private Camera _mainCamera;
    private Vector3 _min, _max;
    private Vector3 _velocity;
    
    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        transform.position = new Vector3(FollowTransform.position.x, FollowTransform.position.y, transform.position.z);
    }

    void Update()
    {
        if (FollowTransform == null)

        if (_isShaking)
        {
            ShakeScreen();
        }

        if (_mainCamera.orthographicSize != _goalZoom)
        {
            MoveCameraZoom();
        }

        var bounds = new Bounds(FollowTransform.transform.position, Vector3.zero);
        var location = bounds.center;
        
        if (_isShaking)
        {
            _preShakePosition = Vector3.SmoothDamp(_preShakePosition, new Vector3(location.x, location.y, _preShakePosition.z),
                ref _velocity, SmoothTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(location.x, location.y, transform.position.z),
                ref _velocity, SmoothTime);
        }
    }
}

