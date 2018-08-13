using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenMusicController : MonoBehaviour
{
    public FMOD.Studio.EventInstance titleScreenMusic;
		
    private void Start()
    {
        titleScreenMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TitleScreen");
        titleScreenMusic.start();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name.Equals("Splash"))
            {
                SceneManager.LoadScene("Mike");
            }
            else
            {
                titleScreenMusic.setParameterValue("ActivateOutro", 1);
                titleScreenMusic.setParameterValue("SplashSongFade", 1);
            }
        }
    }

    public void FadeOutMusic()
    {
        titleScreenMusic.setParameterValue("ActivateOutro", 1);
        titleScreenMusic.setParameterValue("SplashSongFade", 1);
    }
}