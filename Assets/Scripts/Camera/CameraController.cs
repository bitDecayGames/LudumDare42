using System.Collections.Generic;
using UnityEngine;

public partial class CameraController : MonoBehaviour
{
    public List<Transform> FollowTransform;
    public Transform DefaultFollowTransform;

    private const float SmoothTime = .5f;
    private const float DefaultZoom = 6f;

    private float _goalZoom = 6f;

    private Camera _mainCamera;
    private Vector3 _min, _max;
    private Vector3 _velocity;

    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        var centroid = calculateCentroid();
        transform.position = new Vector3(centroid.x, centroid.y, transform.position.z);
    }

    void Update()
    {
        if (_isShaking)
        {
            ShakeScreen();
        }

        if (_mainCamera.orthographicSize != _goalZoom)
        {
            MoveCameraZoom();
        }


        Vector3 location = calculateCentroid();


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

    private Vector3 calculateCentroid()
    {
        if (FollowTransform.Count <= 0) {
            if (DefaultFollowTransform != null && DefaultFollowTransform) return DefaultFollowTransform.position;
            return transform.position;
        }
        var centroid = new Vector3(0, 0, 0);
        if (FollowTransform != null && FollowTransform.Count > 0)
        {
            FollowTransform.ForEach(t =>
            {
                if (t != null) centroid += t.position;
            });
            centroid /= FollowTransform.Count;
        }
        return centroid;
    }
}

