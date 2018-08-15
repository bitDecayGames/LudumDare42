using UnityEngine;

[ExecuteInEditMode]
public class CheckpointDebugger : MonoBehaviour {

	public Transform PathMarkers;
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		var blackHoleNodes = PathMarkers.GetComponentsInChildren<Transform>();
		foreach (Transform gateTransform in transform) {
			CheckpointGate gate = gateTransform.GetComponent<CheckpointGate>();
			if (gate) {
				for (int i = 0; i < blackHoleNodes.Length; i++) {
					if (gate.BlackHoleNode == i) {
						Debug.DrawLine(gate.transform.position, blackHoleNodes[i].position, Color.magenta);
					}
				}
			}
		}
		#endif
	}
}
