using UnityEngine;
using UnityEngine.UI;

public class FadeInFromBlackAfterTimeout : MonoBehaviour {

	public Text Text;
	public float Timeout;
	public float TimeToFadeIn;

	private int state;
	private float _time;
	
	// Use this for initialization
	void Start () {
		_time = Timeout;
		state = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (state > 0) _time -= Time.deltaTime;
		switch (state) {
			case 1:
				if (_time < 0) {
					state += 1;
					_time = TimeToFadeIn;
				}
				break;
			case 2:
				if (_time > 0) {
					var timeRatio = _time / TimeToFadeIn;
					var col = Text.color;
					col.a = 1 - timeRatio;
					Text.color = col;
				}
				break;
			default:
				state = -1;
				break;
		}
	}
}
