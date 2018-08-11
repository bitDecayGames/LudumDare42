using UnityEngine;

public class FollowRotation : MonoBehaviour {
    public Transform Follow;

    [Range(0.0001f, 1)]
    public float smoothing;

    void Update() {
        var cur = transform.rotation.eulerAngles.z;
        var target = Follow.rotation.eulerAngles.z;
        var diff = target - cur;
        if (diff > 180) diff -= 360;
        else if (diff < -180) diff += 360;
        var smoothed = diff * smoothing + cur;
        transform.rotation = Quaternion.Euler(0, 0, smoothed);
    }
}