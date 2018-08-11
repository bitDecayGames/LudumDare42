using UnityEngine;

public class FollowRotation : MonoBehaviour {
    public Transform Follow;

    void Update() {
        transform.rotation.Set(Follow.rotation.x, Follow.rotation.y, Follow.rotation.z, Follow.rotation.w);
    }
}