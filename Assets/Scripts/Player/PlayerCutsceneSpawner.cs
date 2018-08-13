using UnityEngine;

namespace Player
{
    
    public class PlayerCutsceneSpawner : MonoBehaviour
    {

        private CameraController cam;

        public Rigidbody2D SpawnLocationPrefab;
        public Vector2 ShootVector;
        public float ShootDelay;
        public float OpenDelay;
        private Animator hatch;

        private float _startDelay;
        private float _hatchDelay;
        private float _shootTimer;
        private bool shot = false;
        private bool _musicStarted;
        private Rigidbody2D bullet;

        private enum SpawnState
        {
            PRE,
            DELAYEDSTART,
            START,
            SPAWN,
            CLOSE,
            DONE
        }

        private SpawnState currentState;

        // Use this for initialization
        void Start()
        {
            currentState = SpawnState.PRE;
            _hatchDelay = OpenDelay;
            _shootTimer = ShootDelay;
            hatch = GetComponentInChildren<Animator>();
            hatch.Play("Idle");
        }

        public void startSequence()
        {
            var splashScreenMusicController = GameObject.Find("MusicController");
            if (splashScreenMusicController != null)
            {
                _startDelay = 4f;
                splashScreenMusicController.SendMessage("FadeOutMusic");
            }
            currentState = SpawnState.DELAYEDSTART;
        }

        private void OnDestroy()
        {
            if (cam != null)
            {
                cam.FollowTransform.Remove(transform);
            }
        }



        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case SpawnState.PRE:
                    break;
                case SpawnState.DELAYEDSTART:
                    _startDelay -= Time.deltaTime;
                    if (_startDelay <= 0)
                    {
                        currentState = SpawnState.START;
                    }
                    break;
                case SpawnState.START:
                    transform.Find("CamFollow").gameObject.SetActive(true);
                    if (!_musicStarted)
                    {
                        _musicStarted = true;
                        GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().StartMusic();
                    }
                    _hatchDelay -= Time.deltaTime;
                    if (_hatchDelay <= 0)
                    {
                        _hatchDelay = OpenDelay;
                        hatch.Play("Open");
                        var hatchOpenSfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Hatch/OpenHatch");
                        hatchOpenSfx.start();
                        hatchOpenSfx.release();
                        currentState = SpawnState.SPAWN;
                    }
                    break;
                case SpawnState.SPAWN:
                    if (_shootTimer < 0)
                    {
                        bullet = Instantiate(SpawnLocationPrefab);
                        bullet.transform.position = transform.position;
                        bullet.velocity = ShootVector;
                        bullet.GetComponent<SpawnOnDestroy>().OnSpawn.AddListener(playerTransform => {
                            var tmpCam = Camera.main.GetComponent<CameraWarpShader>();
                            tmpCam.setWarpPosition(playerTransform.position);
                            playerTransform.GetComponent<PlayerControls>().onTeleport
                                .AddListener(tmpCam.setWarpPosition);
                        });
                        currentState = SpawnState.CLOSE;
                    }
                    else _shootTimer -= Time.deltaTime;
                    break;
                case SpawnState.CLOSE:
                    _hatchDelay -= Time.deltaTime;
                    if (_hatchDelay <= 0)
                    {
                        hatch.Play("Close");
                        var hatchCloseSfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Hatch/CloseHatch");
                        hatchCloseSfx.start();
                        hatchCloseSfx.release();
                        currentState = SpawnState.DONE;
                    }
                    break;
                case SpawnState.DONE:
                    break;
            }
        }
    }
}
