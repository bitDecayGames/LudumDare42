using UnityEngine;

namespace Player {
	public class TeleportToThing : MonoBehaviour {

		public bool canTeleport = false;
		
		public void Teleport(Transform thing) {
				Debug.Log("Teleport");
			if (canTeleport) {
				transform.position = thing.position;
				canTeleport = false;
			} 
		}
	}
}
