using UnityEngine;

namespace Player {
	public class PlayerCutsceneSpawner : MonoBehaviour {

		public Rigidbody2D SpawnLocationPrefab;
		public Vector2 ShootVector;
		public float ShootDelay;

		private float _shootTimer;
		private bool shot = false;
		private Rigidbody2D bullet;
		
		// Use this for initialization
		void Start () {
			_shootTimer = ShootDelay;
		}
	
		// Update is called once per frame
		void Update () {
			if (!shot) {
				if (_shootTimer < 0) {
					bullet = Instantiate(SpawnLocationPrefab);
					bullet.transform.position = transform.position;
					bullet.velocity = ShootVector;
					shot = true;
					enabled = false;
				}
				else _shootTimer -= Time.deltaTime;
			} else Debug.Log("Hello...");
		}
	}
}
