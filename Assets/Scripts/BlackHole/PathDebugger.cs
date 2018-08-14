using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathDebugger : MonoBehaviour {

	private List<Transform> nodes = new List<Transform>();
	
	void Update () {
		nodes.Clear();
		foreach (Transform child in transform) {
			nodes.Add(child);
		}
		for (int i = 1; i < nodes.Count; i++) Debug.DrawLine(nodes[i - 1].position, nodes[i].position, Color.cyan);
	}
}
