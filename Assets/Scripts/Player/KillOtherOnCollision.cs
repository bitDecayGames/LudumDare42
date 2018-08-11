using UnityEngine;

namespace Player {
	public class KillOtherOnCollision : MonoBehaviour {
		void OnCollisionEnter2D(Collision2D other) {
			Destroy(other.rigidbody.gameObject);		
		}
	}
}
