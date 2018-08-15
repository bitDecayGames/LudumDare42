using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathDebugger : MonoBehaviour {

	private List<Transform> nodes = new List<Transform>();
	
	void Update () {
#if UNITY_EDITOR
		nodes.Clear();
		foreach (Transform child in transform) {
			nodes.Add(child);
		}

		for (int i = 0; i < nodes.Count; i++) {
			if (i > 0) Debug.DrawLine(nodes[i - 1].position, nodes[i].position, Color.cyan);
		}
#endif
	}
}
