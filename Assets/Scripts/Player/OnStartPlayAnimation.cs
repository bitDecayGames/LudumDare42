using UnityEngine;

namespace Player {
	[RequireComponent(typeof(Animator))]
	public class OnStartPlayAnimation : MonoBehaviour {

		public string AnimationName;

		void Awake() {
			Init();
		}

		private void Init() {
			if (AnimationName != null) {
				Debug.Log("Awake: " + AnimationName);
				GetComponent<Animator>().Play(AnimationName);
			}
		}
	}
}
