using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusicButton : MonoBehaviour {

	public string MusicName;
	
	private FMOD.Studio.EventInstance _ambientMusicEvent;


	public void Init() {
		if (string.IsNullOrEmpty(MusicName)) throw new Exception("Music name cannot be empty or null");
		var split = MusicName.Split('/');
		var shortName = split[split.Length - 1]; 
		name = "Music(" + shortName + ")";

		GetComponent<Button>().GetComponentInChildren<Text>().text = shortName;
		_ambientMusicEvent = FMODUnity.RuntimeManager.CreateInstance(MusicName);
	}
	

	public void PlayMusic() {
		_ambientMusicEvent.start();
	}

	public void StopMusic() {
		_ambientMusicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
