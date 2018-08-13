using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class PlayFXButton : MonoBehaviour {

	public string FXName;
	public bool IsLooped;

	public void Init() {
		if (string.IsNullOrEmpty(FXName)) throw new Exception("FX name cannot be empty or null");
		var split = FXName.Split('/');
		var shortName = split[split.Length - 1]; 
		name = "FX(" + shortName + ")";
		GetComponent<Button>().GetComponentInChildren<Text>().text = shortName;
	}

	public void PlayFX() {
		RuntimeManager.PlayOneShot(FXName);
	}

	public void StopFX() {
		
	}
}
