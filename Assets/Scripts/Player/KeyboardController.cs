using System;
using UnityEngine;
public class KeyboardController : MonoBehaviour
{
    private bool _audioSweep;
    private int _direction = 1;
    private float _frequencyRange = 1f;
    private float _speedMultiplier = 5;
    public GameObject BlackHole;
    public GameObject MusicGameObject;

    private MainMusicController _musicController;
    
    private void Start()
    {
        if (BlackHole == null)
        {
            throw new Exception("BlackHole game object not assigned in editor");
        }
        if (MusicGameObject == null)
        {
            throw new Exception("MusicController game object not assigned in editor");
        }

        _musicController = MusicGameObject.GetComponent<MainMusicController>();
        if (_musicController == null)
        {
            throw new Exception("MusicController not found on MusicGameObject");
        }
    }

    void Update()
    {

        CheckForAudioSweep();
        
        if (Math.Abs(Input.GetAxis("Horizontal")) > .001)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _speedMultiplier * Time.deltaTime);
        }
        if (Math.Abs(Input.GetAxis("Vertical")) > .001)
        {
            transform.Translate(Vector3.up * Input.GetAxis("Vertical") * _speedMultiplier * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("BlackHole"))
        {
            var blackHoleController = gameObject.AddComponent<GetSuckedIntoBlackHoleController>();
            blackHoleController.SetBlackHole(BlackHole);
            Destroy(this);
        }
        
        if (other.gameObject.name.Equals("EndTrigger"))
        {        
            _musicController.SetEndTrigger(0);
        }
        
        if (other.gameObject.name.Equals("FadeOut"))
        {        
            _musicController.SetFadeOut(0);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("EndTrigger"))
        {        
            _musicController.SetEndTrigger(1);
        }
        
        if (other.gameObject.name.Equals("FadeOut"))
        {        
            _musicController.SetFadeOut(1);
        }
    }

    private void CheckForAudioSweep()
    {
        
        if (Input.GetKeyDown(KeyCode.B) && !_audioSweep)
        {
            _audioSweep = true;
            _direction = -1;
            _frequencyRange = .99f;
        }

        if (_audioSweep && _frequencyRange != 1f)
        {
            _frequencyRange = Mathf.Clamp(_frequencyRange + .05f * _direction, 0, 1);
            if (_frequencyRange == 0 && _direction == -1)
            {
                _direction = 1;
            }
        }
        else
        {
            _audioSweep = false;
        }

    }
}