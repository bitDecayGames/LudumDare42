using System;
using Player;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour {
	public class Checkpoint {
		public Vector3 playerPosition;
		public int blackHoleStartingNode;

		public Checkpoint(Vector3 pos, int node) {
			playerPosition = pos;
			blackHoleStartingNode = node;
		}
	}

	public static Checkpoint latestCheckpoint = new Checkpoint(new Vector3(45, 14, 0), 21);

	public static void SetLatestCheckpoint(Vector3 playerPos, int blackHoleStartingNode) {
		latestCheckpoint = new Checkpoint(playerPos, blackHoleStartingNode);
	}

	public PlayerControls player;

	public BlackHoleController blackHole;

	public RectTransform ContinueButton;

	public GameObject PathMarkers;

	public BrownHoleEffect ForegroundCamera;

	void Start() {
		ContinueButton.gameObject.SetActive(latestCheckpoint != null);
	}


	public void InitializeFromLatestCheckpoint() {
		if (latestCheckpoint != null) {
			var p = Instantiate(player);
			p.transform.position = latestCheckpoint.playerPosition;
			p.SetPlayerPhase(PlayerControls.AIM_PHASE);
			Camera.main.GetComponent<CameraController>().DefaultFollowTransform = p.transform;

			var b = Instantiate(blackHole);
			b.PathMarkers = PathMarkers;
			b.Init();
			var node = b.SkipToNodeIndex(latestCheckpoint.blackHoleStartingNode);
			node.speed = 1;
			b.transform.position = node.position;

			ForegroundCamera.brownHole = b.transform;
			GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().StartActionMusic();
		}
		else throw new Exception("There was no checkpoint to continue from");
	}
}
