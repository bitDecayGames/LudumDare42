using UnityEngine;

public class OnStartAddToMetric : MonoBehaviour {

	public string MetricKey;
	
	void Start() {
		if (!string.IsNullOrEmpty(MetricKey)) {
			var tracker = Camera.main.GetComponent<MetricTracker>();
			if (tracker) {
				tracker.AddToTracking(MetricKey);
			}
		}
	}
}
