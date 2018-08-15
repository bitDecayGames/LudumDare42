using UnityEngine;

public class CheckpointGate : MonoBehaviour {

	public int BlackHoleNode;

	public void TriggerCheckpoint() {
		CheckpointTracker.SetLatestCheckpoint(transform.position, BlackHoleNode);
	}
}
