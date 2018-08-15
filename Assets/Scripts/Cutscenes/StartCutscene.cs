using System;
using FMODUnity;
using Player;
using UnityEngine;
using Object = System.Object;

public class StartCutscene : MonoBehaviour
{

    public bool CutsceneIsPlaying;
    
    public Canvas CutscenePrefab;
    public BlackHoleController BlackHolePrefab;
    public DestroyAfterTimeLimit DestroyTimer;
    public Transform BlackHoleSpawnPoint;
    public Transform PlayerSpawnPoint;
    public Animator CrystalRoomAnimator;
    public Transform WhiteFlashPrefab;
    public BlackHoleGrower BlackHoleGrowerPrefab;

    public GameObject PathMarkers;
    public BrownHoleEffect ForegroundCamera;
    
    private float originalAnimatorSpeed;
    private BlackHoleGrower grower;

    private PlayerControls player;
    private bool started = false;

    private bool _speedingup;
    private float _timeSpentSpeedingUp;
    
    void Start() {
        if (!CutscenePrefab) throw new Exception("Need to link to the cutscene prefab (rotoscope animation object)");
        if (!BlackHolePrefab) throw new Exception("Need to link to the black hole prefab");
        if (!PathMarkers) throw new Exception("Need to link to the path markers object with the list of nodes for the black hole");
        if (!BlackHoleSpawnPoint) throw new Exception("Need to link to a transform that marks where the black hole will initially spawn");
        if (!CrystalRoomAnimator) throw new Exception("Need to link to an animator for the crystal room animation");
        if (!BlackHoleGrowerPrefab) throw new Exception("Need to link a black hole grower prefab");
    }

    private void Update()
    {
        if (_speedingup)
        {
            
            _timeSpentSpeedingUp += Time.deltaTime;

            if (_timeSpentSpeedingUp < 2f)
            {
                CrystalRoomAnimator.speed += .0003f;
            }
            else
            {
                CrystalRoomAnimator.speed += .01f;
            }
            
        }
    }

    public void StartBrokenPanelRotoscope() {
        CutsceneIsPlaying = true;
        if (!started) {
            GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().FadeOutAmbientSong();
            started = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            player.SetPlayerPhase(PlayerControls.DISABLED_PHASE);
            player.DestroyActiveTeleBall();
            player.transform.position = PlayerSpawnPoint.position;
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            var cutscene = Instantiate(CutscenePrefab);
            cutscene.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndBrokenPanelRotoscope);
            cutscene.transform.position = new Vector3();
        }
    }

    public void EndBrokenPanelRotoscope(Transform t) {
        Camera.main.GetComponent<CameraController>().DefaultFollowTransform = BlackHoleSpawnPoint;
        StartSpeedingCrystal();
    }

    public void StartSpeedingCrystal()
    {
        _speedingup = true;
        GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().SpeedUpCrystal();
        originalAnimatorSpeed = CrystalRoomAnimator.speed;
        var timer = Instantiate(DestroyTimer, transform);
        timer.TimeLimit = 12;
        timer.RefreshTimer();
        timer.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndSpeedingCrystal);
    }

    public void EndSpeedingCrystal(Transform t)
    {
        _speedingup = false;
        CrystalRoomAnimator.speed = originalAnimatorSpeed;
        RuntimeManager.PlayOneShot("event:/SFX/Explosions/CrystalExplosion");
        GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().StopCrystalSound();
        CreateFlash();
    }

    public void CreateFlash() {
        var timer = Instantiate(DestroyTimer, transform);
        timer.TimeLimit = 2.5f;
        timer.RefreshTimer();
        timer.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndFlash);
        Instantiate(WhiteFlashPrefab);
    }

    public void EndFlash(Transform t) {
        StartBrokenCrystalRoom();
    }
    
    public void StartBrokenCrystalRoom() {
        CrystalRoomAnimator.Play("BrokenCrystalRoomAnimation");
        var timer = Instantiate(DestroyTimer, transform);
        timer.TimeLimit = 1.5f;
        timer.RefreshTimer();
        timer.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndBrokenCrystalRoom);
    }

    public void EndBrokenCrystalRoom(Transform t) {
        StartBlackHole();
    }

    public void StartBlackHole() {
        RuntimeManager.PlayOneShot("event:/SFX/BlackHoleGrow/BlackHoleGrow");
        grower = Instantiate(BlackHoleGrowerPrefab);
        grower.transform.position = BlackHoleSpawnPoint.position;
        ForegroundCamera.brownHole = grower.transform;
        grower.brownHole = ForegroundCamera;
        grower.OnDoneGrowing.AddListener(DelayBlackHoleEnding);
        grower.StartGrowing();
    }

    public void DelayBlackHoleEnding()
    {
        var timer = Instantiate(DestroyTimer, transform);
        timer.TimeLimit = 2;
        timer.RefreshTimer();
        timer.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndBlackHoleInitialization);
    }
    
    public void EndBlackHoleInitialization(Transform t)
    {
        CutsceneIsPlaying = false;
        Destroy(grower);
        GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().StartActionMusic();
        var blackHole = Instantiate(BlackHolePrefab);
        blackHole.transform.position = BlackHoleSpawnPoint.position;
        blackHole.PathMarkers = PathMarkers;
        blackHole.Init();
        ForegroundCamera.brownHole = blackHole.transform;
        player.SetPlayerPhase(PlayerControls.AIM_PHASE);
        Camera.main.GetComponent<CameraController>().DefaultFollowTransform = player.transform;
        Destroy(gameObject);
    }
}
