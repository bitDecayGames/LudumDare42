using UnityEngine;

namespace Player {
	public class AddMyselfToCameraFollowAfterTimeout : MonoBehaviour {
		public float Timeout;
		public bool isDefaultFollow = false;
		
		private CameraController cam;
		private bool added = false;
		private float _timeout;
		
		void OnEnable() {
			if (!added) {
				cam = Camera.main.GetComponent<CameraController>();
				if (cam != null) {
					_timeout = Timeout;
					
				}
			}
		}

		void Update() {
			if (!added) {
				if (_timeout < 0) Add();
				else _timeout -= Time.deltaTime;
			}
		}

		private void OnDisable() {
			Remove();
		}

		private void OnDestroy() {
			Remove();
		}

		private void Add() {
			if (isDefaultFollow) cam.DefaultFollowTransform = transform;
			else cam.FollowTransform.Add(transform);
			added = true;
		}

		private void Remove() {
			if (added && cam != null) {
				added = false;
				if (isDefaultFollow) cam.DefaultFollowTransform = null;
				else cam.FollowTransform.Remove(transform);
			}
		}
	}
}
