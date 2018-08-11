using System;
using UnityEngine;

namespace Environments {
	public class InverseGravityEnvironment : TriggerEnvironment {

		protected override void ApplyEffect(Collider2D thing) {
			thing.attachedRigidbody.gravityScale = Math.Abs(thing.attachedRigidbody.gravityScale) * -1;
		}

		protected override void RemoveEffect(Collider2D thing) {
			thing.attachedRigidbody.gravityScale = Math.Abs(thing.attachedRigidbody.gravityScale);
		}
	}
}
