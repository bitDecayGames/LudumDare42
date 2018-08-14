using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndCutscene : MonoBehaviour {

	public BrownHoleEffect ForegroundCamera;

	public Transform MovePlayerToPoint;
	public Transform MoveBlackHoleToPointA;
	public Transform MoveBlackHoleToPointB;
	public SpriteRenderer FadeToBlack;
	public Rigidbody2D FakeTeleBall;
	public CreditMetadata CreditMetadataPrefab;
	public RuntimeAnimatorController EndPlayerAnimator;
	public Transform Sparkles;

	private PlayerControls player;
	private CameraController cam;

	private int state = 0;

	private float timeRatio;
	private int waitTimeMS = 1;
	private int curTimeMS = 0;

	private Vector3 targetBlackHoleOriginalPosition;
	private Vector3 targetBlackHolePath;

	void Start() {
		if (!ForegroundCamera) throw new Exception("Need to hook the foreground camera into me");	
	}

	public void StartEndCutscene() {
		// turn off sparkles
		Sparkles.gameObject.SetActive(false);
		// kill black hole controller
		GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().SetFadeOut(0);
		var bhCtrl = FindObjectOfType<BlackHoleController>();
		if (bhCtrl) Destroy(bhCtrl);

		// move player to position
		player = GameObject.Find("Player(Clone)").GetComponent<PlayerControls>();
		player.ForceMoveTeleBallToPosition(MovePlayerToPoint.position);
		player.ForceTeleportToBall();
		player.transform.rotation = Quaternion.Euler(0, 0, 0);
		// disable player
		player.SetPlayerPhase(PlayerControls.DISABLED_PHASE);
		Destroy(player.GetComponentInChildren<AlwaysFaceTheMouse>());

		// move black hole to A
		ForegroundCamera.brownHole = MoveBlackHoleToPointA;

		targetBlackHoleOriginalPosition = MoveBlackHoleToPointA.position;
		targetBlackHolePath = MoveBlackHoleToPointB.position - MoveBlackHoleToPointA.position;

		cam = Camera.main.GetComponent<CameraController>();
		
		SetState(1);
	}

	private void SetState(int state) {
		switch (state) {
			case 1:
				// screen shake for 2 seconds
				waitTimeMS = 2000;
				cam.InitiateScreenShake(waitTimeMS / 1000f, .1f);
				// TODO: Tanner: Just entered the cutscene
				break;
			case 2:
				// rumble shake for 8 seconds
				waitTimeMS = 6000;
				cam.InitiateScreenShake(waitTimeMS / 1000f, .06f);
				// turn left, turn right, turn left, turn right, go to throw
				var animator = player.GetComponentInChildren<Animator>();
				animator.runtimeAnimatorController = EndPlayerAnimator;
				animator.Play("EndCutscene");
				// TODO: Tanner: getting nervous, about to throw the ball
				break;
			case 3:
				waitTimeMS = 300;
				cam.InitiateScreenShake(waitTimeMS / 500f, 0.01f);
				// shoot ball to the right
				var fakeBall = Instantiate(FakeTeleBall);
				var fakeBallThrowPosition = player.transform.position;
				fakeBallThrowPosition.x += .4f;
				fakeBallThrowPosition.y += .5f;
				fakeBall.transform.position = fakeBallThrowPosition;
				fakeBall.velocity = new Vector3(12, 6, 0);
				fakeBall.angularVelocity = 200f;
				// slow time
				Time.timeScale = 0.1F;
				Time.fixedDeltaTime = 0.02F * Time.timeScale;
				// TODO: Tanner: ball starts shooting off into space, time slows
				var creditsMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/CreditsSong");
				creditsMusic.start();
				creditsMusic.release();
				break;
			case 4:
				waitTimeMS = 275;
				// TODO: Tanner: fade to black
				break;
			case 5:
				// TODO: Tanner: load credits screen here
				Time.timeScale = 1F;
				Time.fixedDeltaTime = 0.02F * Time.timeScale;
				// set winning flag to true
				var creditMetadata = Instantiate(CreditMetadataPrefab);
				creditMetadata.cameFromTitle = false;
				// load credits screen
				SceneManager.LoadScene("Credits");
				break;
		}

		this.state = state;
		curTimeMS = 0;
	}

	
	void Update() {
		if (state > 0) {
			curTimeMS += (int)(Time.deltaTime * 1000);
			if (curTimeMS > waitTimeMS) curTimeMS = waitTimeMS;
			timeRatio = curTimeMS / (float) waitTimeMS;
		}

		switch (state) {
			case 1:
				// move black hole from A to B
				MoveBlackHoleToPointA.position = targetBlackHolePath * timeRatio + targetBlackHoleOriginalPosition;
				break;
			case 2:
			case 3:
				MoveBlackHoleToPointA.position += new Vector3(0.005f, 0, 0);
				break;
			case 4:
				// fade to black
				var color = FadeToBlack.color;
				color.a = timeRatio;
				FadeToBlack.color = color;
				break;
			default:
				state = -1;
				break;
		}

		if (curTimeMS == waitTimeMS) {
			SetState(state + 1);
		}
	}
}
