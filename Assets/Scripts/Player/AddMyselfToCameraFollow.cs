﻿using UnityEngine;

namespace Player {
	public class AddMyselfToCameraFollow : MonoBehaviour {
		public bool isDefaultFollow = false;
		
		private CameraController cam;
		private bool added = false;
		
		void OnEnable() {
			if (!added) {
				cam = Camera.main.GetComponent<CameraController>();
				if (cam != null) {
					if (isDefaultFollow) cam.DefaultFollowTransform = transform;
					else cam.FollowTransform.Add(transform);
					added = true;
				}
			}
		}

		private void OnDisable() {
			Remove();
		}

		private void OnDestroy() {
			Remove();
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
