using System;
using UnityEngine;

public class MainMusicController : MonoBehaviour
{
    private const string ProximityProperty = "Proximity";
    private const string EndTriggerProperty = "EndTrigger";
    private const string FadeOutProperty = "FadeOut";

    private bool _musicStarted;
     
    public Transform ProximityGameObjectTransform1;
    public Transform ProximityGameObjectTransform2;

    [FMODUnity.EventRef] private string _actionMusic = "event:/Music/ArcadeTheme/MainThemeAction";
    private FMOD.Studio.EventInstance actionMusicEvent;
    
    [FMODUnity.EventRef] private string _ambientMusic = "event:/Music/SpaceAmbience";
    private FMOD.Studio.EventInstance _ambientMusicEvent;
    
    [FMODUnity.EventRef] private string _crystalAmbience = "event:/SFX/Crystal/Crystal";
    private FMOD.Studio.EventInstance _crystalAmbienceEvent;
    
    public void SetEndTrigger(float value)
    {   
        actionMusicEvent.setParameterValue(EndTriggerProperty, value);
    }

    public void SetFadeOut(float value)
    {
        actionMusicEvent.setParameterValue(FadeOutProperty, value);
    }
    
    public void StartMusic()
    {
        _musicStarted = true;
        _ambientMusicEvent = FMODUnity.RuntimeManager.CreateInstance(_ambientMusic);
        _ambientMusicEvent.start();
        
        _crystalAmbienceEvent = FMODUnity.RuntimeManager.CreateInstance(_crystalAmbience);
        _crystalAmbienceEvent.start();
    }

    public void FadeOutAmbientSong()
    {
        _ambientMusicEvent.setParameterValue("FadeSong", 1);
    }

    public void SpeedUpCrystal()
    {
        _crystalAmbienceEvent.setParameterValue("Speedup", 1);
    }

    public void StopCrystalSound()
    {
        _crystalAmbienceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    
    private void Update()
    {
        if (!_musicStarted)
        {
            return;
        }
        
        if (ProximityGameObjectTransform1 == null)
        {
            ProximityGameObjectTransform1 = Camera.main.transform;
        }
        
        float distance = Mathf.Abs(Vector3.Distance(new Vector3(ProximityGameObjectTransform1.position.x, ProximityGameObjectTransform1.position.y, 0), ProximityGameObjectTransform2.position));
        _crystalAmbienceEvent.setParameterValue(ProximityProperty, Mathf.Clamp(distance, 5f, 15f));
    }
}