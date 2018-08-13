using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSceneOnClick : MonoBehaviour {

	public string SceneToGoTo;
	
	void Start() {
		GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(SceneToGoTo));
	}
}
