using UnityEngine;

namespace Player {
	public class DestroyAfterTimeLimit : MonoBehaviour {

		public float TimeLimit;

		private float _timeLimit;
		
		void Start () {
			_timeLimit = TimeLimit;
		}
	
		void Update () {
			if (_timeLimit < 0) Destroy(gameObject);
			else _timeLimit -= Time.deltaTime;
		}
	}
}
