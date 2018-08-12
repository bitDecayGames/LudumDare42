using System;
using UnityEngine;

public class MainMusicController : MonoBehaviour
{
    private const string ProximityProperty = "Proximity";
    private const string EndTriggerProperty = "EndTrigger";
    private const string FadeOutProperty = "FadeOut";
    
    public float EffectiveDistance = 10;    
    public Transform ProximityGameObjectTransform1;
    public Transform ProximityGameObjectTransform2;

    private FMODUnity.StudioEventEmitter _eventEmitter;
    [FMODUnity.EventRef] private string _music = "event:/Music/ArcadeTheme/MainThemeAction";
    private FMOD.Studio.EventInstance musicEvent;
    
    public void SetEndTrigger(float value)
    {   
        musicEvent.setParameterValue(EndTriggerProperty, value);
    }

    public void SetFadeOut(float value)
    {
        musicEvent.setParameterValue(FadeOutProperty, value);
    }
    
    public void Start()
    {
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
        musicEvent.start();
    }
    
    private void Update()
    {
        if (ProximityGameObjectTransform1 == null || ProximityGameObjectTransform2 == null)
        {
            return;
        }
        
        float distance = Vector3.Distance(ProximityGameObjectTransform1.position, ProximityGameObjectTransform2.position);
        musicEvent.setParameterValue(ProximityProperty, Mathf.Clamp(distance, 0f, EffectiveDistance));
    }
}