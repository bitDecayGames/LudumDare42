using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player {
	public class SpawnOnDestroy : MonoBehaviour {
		[Serializable]
		public class OnSpawnEvent : UnityEvent<Transform> {}
	
		public Transform Spawn;
		public bool shouldSpawn = true;
		public OnSpawnEvent OnSpawn;

		private void OnDestroy() {
			if (shouldSpawn) {
				var spawn = Instantiate(Spawn);
				spawn.position = transform.position;
				spawn.rotation = transform.rotation;
				OnSpawn.Invoke(spawn);
			}
		}
	}
}
