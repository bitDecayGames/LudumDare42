using System;
using UnityEngine;

public class AmbientMusicController : MonoBehaviour
{
    private const string ProximityProperty = "Proximity";
    
    public float EffectiveDistance = 10;    
    public Transform GameObjectTransform1;
    public Transform GameObjectTransform2;

    private FMODUnity.StudioEventEmitter _eventEmitter;
    [FMODUnity.EventRef] private string _music = "event:/Music/SpaceAmbience";
    private FMOD.Studio.EventInstance musicEvent;
    
    public void Start()
    {
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
        musicEvent.start();
    }
    
    private void Update()
    {
        if (GameObjectTransform1 == null || GameObjectTransform2 == null)
        {
            return;
        }
        
        float distance = Vector3.Distance(GameObjectTransform1.position, GameObjectTransform2.position);
        musicEvent.setParameterValue(ProximityProperty, Mathf.Clamp(distance, 0f, EffectiveDistance));
    }
}