using UnityEngine;

public class OnDestroyAddToMetric : MonoBehaviour {

	public string MetricKey;
	
	private void OnDestroy() {
		if (!string.IsNullOrEmpty(MetricKey)) {
			var tracker = Camera.main.GetComponent<MetricTracker>();
			if (tracker) {
				tracker.AddToTracking(MetricKey);
			}
		}
	}
}
