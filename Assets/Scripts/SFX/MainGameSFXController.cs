using FMODUnity;
using UnityEngine;

public class MainGameSFXController : MonoBehaviour
{
    public void PlayTeleportSound(bool firstTime = false)
    {
        
        var teleportSound = RuntimeManager.CreateInstance("event:/SFX/Teleport/Teleport");
        if (firstTime)
        {
            teleportSound.setParameterValue("FirstTime", 1);
        }
        teleportSound.start();
        teleportSound.release();
    }

    public void PlayMenuSound()
    {
        RuntimeManager.PlayOneShot("event:/SFX/Menu/Menu");
    }
    
    public void PlayBeepShort()
    {
        RuntimeManager.PlayOneShot("event:/SFX/Ball/Beep/Beep");
    }

    public void PlayBeepLong()
    {
        RuntimeManager.PlayOneShot("event:/SFX/Ball/Beep/BeepLong");
    }
}