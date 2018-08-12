using UnityEngine;

namespace Player {
	public class AddMyselfToCameraFollow : MonoBehaviour {

		private CameraController cam;
	
		void Start () {
			cam = Camera.main.GetComponent<CameraController>();
			if (cam != null) {
				cam.FollowTransform.Add(transform);
			}
		}

		private void OnDestroy() {
			if (cam != null) {
				cam.FollowTransform.Remove(transform);
			}
		}
	}
}
