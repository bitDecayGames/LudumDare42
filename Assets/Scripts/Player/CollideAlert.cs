using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player {
	public class CollideAlert : MonoBehaviour {
		[Serializable]
		public class OnCollisionEvent : UnityEvent<Collision2D> {}
		
		public OnCollisionEvent OnCollideEnter = new OnCollisionEvent();
		public OnCollisionEvent OnCollideExit = new OnCollisionEvent();
		
		void OnCollisionEnter2D(Collision2D other) {
			OnCollideEnter.Invoke(other);
		}
		
		void OnCollisionStay2D(Collision2D other) {
			OnCollideEnter.Invoke(other);
		}

		void OnCollisionExit2D(Collision2D other) {
			OnCollideExit.Invoke(other);
		}
	}
}
