using System;
using Player;
using UnityEngine;

public class StartCutscene : MonoBehaviour {

    public Canvas CutscenePrefab;

    public BlackHoleController BlackHolePrefab;

    public GameObject PathMarkers;

    public BrownHoleEffect ForegroundCamera;

    public Transform BlackHoleSpawnPoint;

    private PlayerControls player;
    
    void Start() {
        if (!CutscenePrefab) throw new Exception("Need to link to the cutscene prefab (rotoscope animation object)");
        if (!BlackHolePrefab) throw new Exception("Need to link to the black hole prefab");
        if (!PathMarkers) throw new Exception("Need to link to the path markers object with the list of nodes for the black hole");
        if (!BlackHoleSpawnPoint) throw new Exception("Need to link to a transform that marks where the black hole will initially spawn");
    }

    public void StartBrokenPanelRotoscope() {
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

    public void EndBrokenPanelRotoscope(Transform t) {
        var blackHole = Instantiate(BlackHolePrefab);
        blackHole.transform.position = BlackHoleSpawnPoint.position;
        blackHole.PathMarkers = PathMarkers;
        ForegroundCamera.brownHole = blackHole.transform;
        Destroy(gameObject);
        player.SetPlayerPhase(PlayerControls.AIM_PHASE);
    }
}
