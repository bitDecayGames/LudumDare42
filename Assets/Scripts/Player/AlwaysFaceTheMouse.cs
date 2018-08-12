using System;
using UnityEngine;

namespace Player {
	public class AlwaysFaceTheMouse : MonoBehaviour {

		private SpriteRenderer spriteRenderer;
		void Start () {
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			if (!spriteRenderer) throw new Exception("Couldn't find SpriteRenderer on any child objects");
		}
		
		void Update () {
			spriteRenderer.flipX = !facingRight();
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
