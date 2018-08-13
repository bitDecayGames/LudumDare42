using FMOD.Studio;
using UnityEngine;

public class MainGameSFXController : MonoBehaviour
{
    private EventInstance _teleportSound;
    private EventInstance _menuSound;
    private void Start()
    {
        _teleportSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Teleport/Teleport");
        _menuSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Menu/Menu");
    }

    public void PlayTeleportSound()
    {
        _teleportSound.start();
    }

    public void PlayMenuSound()
    {
        _menuSound.start();
        _menuSound.release();
    }
}