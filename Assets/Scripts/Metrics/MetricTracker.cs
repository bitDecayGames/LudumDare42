using System.Collections.Generic;
using UnityEngine;

public class MetricTracker : MonoBehaviour {
	public const string TELEPORT = "TELEPORT";
	public const string BALLS_THROWN = "BALLS_THROWN";
	public const string BALL_TIME_OUT = "BALL_TIME_OUT";
	public const string BALL_DEATH = "BALL_DEATH";
	public const string CAUGHT_BY_BLACK_HOLE = "CAUGHT_BY_BLACK_HOLE";
	public const string BOUNCE = "BOUNCE";
	public const string SLIDE = "SLIDE";

	private Dictionary<string, int> metrics = new Dictionary<string, int>();

	public int Get(string key) {
		return Get(key, 0);
	}
	
	public int Get(string key, int defaultValue) {
		if (metrics.ContainsKey(key)) return metrics[key];
		return defaultValue;
	}
	
	public void AddToTracking(string key, int amount = 1) {
		if (!metrics.ContainsKey(key)) metrics[key] = amount;
		else metrics[key] += amount;
	}
}
