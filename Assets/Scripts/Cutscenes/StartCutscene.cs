using UnityEngine;

public class StartCutscene : MonoBehaviour {

    public Canvas CutscenePrefab;

    public void OnStartCutscene() {
        var cutscene = Instantiate(CutscenePrefab);
        cutscene.transform.position = new Vector3();
        Destroy(gameObject);
    }
}
