using UnityEngine;

namespace Environments {
	public class WaterEnvironment : TriggerEnvironment {

		protected override void ApplyEffect(Collider2D thing) {
			thing.attachedRigidbody.drag = 4;
		}

		protected override void RemoveEffect(Collider2D thing) {
			thing.attachedRigidbody.drag = 0;
		}
	}
}
