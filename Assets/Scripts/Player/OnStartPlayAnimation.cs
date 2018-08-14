using FMODUnity;
using UnityEngine;

namespace Player {
	[RequireComponent(typeof(Animator))]
	public class OnStartPlayAnimation : MonoBehaviour {

		public string AnimationName;

		public float delay = 0;

		void Awake() {
			Init();
		}

		private void Init() {
			if (AnimationName != null) {
				GetComponent<Animator>().Play(AnimationName, 0, delay);
			}
		}
		
		public void PlayBreak()
		{
			RuntimeManager.PlayOneShot("event:/SFX/ButtonBreak/ButtonBreakNew");
		}
	}
}
