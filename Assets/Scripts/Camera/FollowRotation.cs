using UnityEngine;

public class FollowRotation : MonoBehaviour {
    public Transform Follow;

    void Update() {
        transform.rotation = Quaternion.Euler(0, 0, Follow.rotation.eulerAngles.z);
    }
}