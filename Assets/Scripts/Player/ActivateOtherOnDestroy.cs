using UnityEngine;

namespace Player {
	public class ActivateOtherOnDestroy : MonoBehaviour {
	
		public Transform ToActivate;

		private void OnDestroy() {
			if (ToActivate) ToActivate.gameObject.SetActive(true);
		}
	}
}
