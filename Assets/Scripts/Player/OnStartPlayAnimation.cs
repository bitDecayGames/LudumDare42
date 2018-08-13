﻿using FMODUnity;
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
				GetComponent<Animator>().Play(AnimationName);
			}
		}
		
		public void PlayBreak()
		{
			RuntimeManager.PlayOneShot("event:/SFX/ButtonBreak/ButtonBreakNew");
		}
	}
}
