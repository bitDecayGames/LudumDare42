using UnityEngine;

public class BlackHoleDemo : MonoBehaviour {
	public Shader BlackHoleShader;
	public Shader BrownHoleShader;
	
	private Transform cam;
	private BrownHoleEffect brown;
	private BlackHoleEffect black;
	private bool switched = false;
	
	void Start () {
		brown = Camera.main.GetComponentInChildren<BrownHoleEffect>();
		cam = brown.transform;
		brown.brownHole = transform;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (switched)
				SwitchToBrownHole();
			else 
				SwitchToBlackHole();	
		}
	}

	void SwitchToBlackHole() {
		switched = true;
		Destroy(brown);
		black = cam.gameObject.AddComponent<BlackHoleEffect>();
		black.blackHole = transform;
		black.blackHoleShader = BlackHoleShader;
		black.radius = .4f;
		black.distance = .58f;
		black.black = .27f;
	}

	void SwitchToBrownHole() {
		switched = false;
		Destroy(black);
		brown = cam.gameObject.AddComponent<BrownHoleEffect>();
		brown.brownHole = transform;
		brown.brownHoleShader = BrownHoleShader;
		brown.radius = .63f;
		brown.black = .27f;
	}
}
