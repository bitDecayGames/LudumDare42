using System;
using FMODUnity;
using Player;
using UnityEngine;
using Object = System.Object;

public class StartCutscene : MonoBehaviour {

    public Canvas CutscenePrefab;
    public BlackHoleController BlackHolePrefab;
    public DestroyAfterTimeLimit DestroyTimer;
    public Transform BlackHoleSpawnPoint;
    public Animator CrystalRoomAnimator;
    public Transform WhiteFlashPrefab;
    public BlackHoleGrower BlackHoleGrowerPrefab;

    public GameObject PathMarkers;
    public BrownHoleEffect ForegroundCamera;
    
    private float originalAnimatorSpeed;

    private PlayerControls player;
    private bool started = false;
    
    void Start() {
        if (!CutscenePrefab) throw new Exception("Need to link to the cutscene prefab (rotoscope animation object)");
        if (!BlackHolePrefab) throw new Exception("Need to link to the black hole prefab");
        if (!PathMarkers) throw new Exception("Need to link to the path markers object with the list of nodes for the black hole");
        if (!BlackHoleSpawnPoint) throw new Exception("Need to link to a transform that marks where the black hole will initially spawn");
        if (!CrystalRoomAnimator) throw new Exception("Need to link to an animator for the crystal room animation");
        if (!BlackHoleGrowerPrefab) throw new Exception("Need to link a black hole grower prefab");
    }

    public void StartBrokenPanelRotoscope() {
        if (!started) {
            GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().FadeOutAmbientSong();
            started = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            player.SetPlayerPhase(PlayerControls.DISABLED_PHASE);
            player.DestroyActiveTeleBall();
            var playerPos = player.transform.position;
            playerPos.x = transform.position.x;
            player.transform.position = playerPos;

            var cutscene = Instantiate(CutscenePrefab);
            cutscene.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndBrokenPanelRotoscope);
            cutscene.transform.position = new Vector3();
        }
    }

    public void EndBrokenPanelRotoscope(Transform t) {
        Camera.main.GetComponent<CameraController>().DefaultFollowTransform = BlackHoleSpawnPoint;
        StartSpeedingCrystal();
    }

    public void StartSpeedingCrystal() {
        GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().SpeedUpCrystal();
        originalAnimatorSpeed = CrystalRoomAnimator.speed;
        CrystalRoomAnimator.speed *= 4;
        var timer = Instantiate(DestroyTimer, transform);
        timer.TimeLimit = 12;
        timer.RefreshTimer();
        timer.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndSpeedingCrystal);
    }

    public void EndSpeedingCrystal(Transform t) {
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
        timer.TimeLimit = 5;
        timer.RefreshTimer();
        timer.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(EndBrokenCrystalRoom);
    }

    public void EndBrokenCrystalRoom(Transform t) {
        StartBlackHole();
    }

    public void StartBlackHole() {
        var grower = Instantiate(BlackHoleGrowerPrefab);
        grower.transform.position = BlackHoleSpawnPoint.position;
        ForegroundCamera.brownHole = grower.transform;
        grower.brownHole = ForegroundCamera;
        grower.OnDoneGrowing.AddListener(EndBlackHoleInitialization);
        grower.StartGrowing();
    }

    public void EndBlackHoleInitialization() {
        GameObject.Find("MainGameMusicController").GetComponent<MainMusicController>().StartActionMusic();
        var blackHole = Instantiate(BlackHolePrefab);
        blackHole.transform.position = BlackHoleSpawnPoint.position;
        blackHole.PathMarkers = PathMarkers;
        ForegroundCamera.brownHole = blackHole.transform;
        
        player.SetPlayerPhase(PlayerControls.AIM_PHASE);
        Camera.main.GetComponent<CameraController>().DefaultFollowTransform = player.transform;
        Destroy(gameObject);
    }
}
