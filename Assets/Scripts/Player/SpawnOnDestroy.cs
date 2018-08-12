using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player {
	public class SpawnOnDestroy : MonoBehaviour {
		[Serializable]
		public class OnSpawnEvent : UnityEvent<Transform> {}
	
		public Transform Spawn;
		public bool shouldSpawn = true;
		public bool shareParent = false;
		public OnSpawnEvent OnSpawn;

		private void OnDestroy() {
			if (shouldSpawn) {
				Transform spawn;
				if (shareParent) spawn = Instantiate(Spawn, transform.parent);
				else spawn = Instantiate(Spawn);
				spawn.position = transform.position;
				spawn.rotation = transform.rotation;
				OnSpawn.Invoke(spawn);
			}
		}
	}
}
