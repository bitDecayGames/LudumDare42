using System;
using UnityEngine;

namespace Player {
	public class AlwaysFaceTheMouse : MonoBehaviour {

		public SpriteRenderer spriteRenderer;
		void Start () {
			if (!spriteRenderer) throw new Exception("Couldn't find SpriteRenderer on any child objects");
		}
		
		void Update () {
			var result = !facingRight();
			spriteRenderer.flipX = result;
		}

		private bool facingRight() {
			var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var toMouse = mouse - transform.position;
			toMouse.z = 0;
			toMouse.Normalize();
			float angle = Vector3.Dot(transform.right, toMouse);
			return angle > 0;
		}
	}
}
