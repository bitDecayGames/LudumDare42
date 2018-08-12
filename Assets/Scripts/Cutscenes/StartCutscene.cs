using UnityEngine;

public class StartCutscene : MonoBehaviour {

    public Canvas CutscenePrefab;

    public BlackHoleController BlackHolePrefab;

    public GameObject PathMarkers;

    public BrownHoleEffect ForegroundCamera;

    public Transform BlackHoleSpawnPoint;

    public void OnStartCutscene() {
        var cutscene = Instantiate(CutscenePrefab);
        cutscene.GetComponent<OnDestroyCallEvent>().OnDestroyed.AddListener(OnEndCutscene);
        cutscene.transform.position = new Vector3();
    }

    public void OnEndCutscene(Transform t) {
        var blackHole = Instantiate(BlackHolePrefab);
        blackHole.transform.position = BlackHoleSpawnPoint.position;
        blackHole.PathMarkers = PathMarkers;
        ForegroundCamera.brownHole = blackHole.transform;
        Destroy(gameObject);
    }
}
