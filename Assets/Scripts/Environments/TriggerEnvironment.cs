using System.Collections.Generic;
using UnityEngine;

namespace Environments {
	public abstract class TriggerEnvironment : MonoBehaviour {

		private List<Collider2D> actives = new List<Collider2D>();
		
		private void OnTriggerEnter2D(Collider2D other) {
			ApplyEffect(other);
		}
		
		private void OnTriggerExit2D(Collider2D other) {
			RemoveEffect(other);
		}
		
		protected abstract void ApplyEffect(Collider2D thing);
		protected abstract void RemoveEffect(Collider2D thing);
	}
}
