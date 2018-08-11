using System;
using UnityEngine;

namespace Player {
	public class FadeToDeath : MonoBehaviour {
		public float timeToLive = 1f;
		private float currentTimeToLive = 0f;
		private SpriteRenderer sprite;

		void Start() {
			if (timeToLive <= 0) throw new Exception("Don't set time to live as less or equal to 0");
			currentTimeToLive = timeToLive;
			sprite = GetComponent<SpriteRenderer>();
		}
		
		void Update () {
			if (currentTimeToLive < 0) {
				Destroy(gameObject);
			}
			else {
				currentTimeToLive -= Time.deltaTime;
				var col = sprite.color;
				col.a = Math.Max(currentTimeToLive / timeToLive, 0);
				sprite.color = col;
			}
		}
	}
}
