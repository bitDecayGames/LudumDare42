using UnityEngine;
using UnityEngine.Events;

public class BlackHoleGrower : MonoBehaviour {

	public float timeToGrow = 1;
	
	[HideInInspector]
	public BrownHoleEffect brownHole;

	[HideInInspector] public UnityEvent OnDoneGrowing = new UnityEvent();

	private bool started = false;
	private bool isDone = false;
	private float _timer;

	private float originalRadius;
	private float originalBlack;
	
	
	public void StartGrowing() {
		started = true;
		_timer = 0.01f;
		originalRadius = brownHole.radius;
		brownHole.radius = 0;
		originalBlack = brownHole.black;
		brownHole.black = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			if (!isDone) {
				_timer += Time.deltaTime;
				if (_timer > timeToGrow) {
					_timer = timeToGrow;
					isDone = true;
				}
				var timerRatio = _timer / timeToGrow;
				brownHole.radius = originalRadius * timerRatio;
				brownHole.black = originalBlack * timerRatio;
				if (isDone) {
					OnDoneGrowing.Invoke();
					Destroy(gameObject);
				} 
			}
		}
	}
}
