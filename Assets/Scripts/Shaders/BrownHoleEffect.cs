using UnityEngine;

public class BrownHoleEffect : MonoBehaviour {

	public Shader brownHoleShader;
	public Transform brownHole;
	public float radius = .5f;
	public float black = .5f;

	[HideInInspector]
	public float ratio;

	private Camera cam;
	private Material _material;
	private Vector3 wtsp;
	private Vector2 pos;

	public Material material {
		get {
			if (_material == null) {
				_material = new Material(brownHoleShader);
				_material.hideFlags = HideFlags.DontSave;
			}
			return _material;
		}
	}

	void OnEnable() {
		cam = GetComponent<Camera>();
		ratio = 1f / cam.aspect;
	}

	private void OnDisable() {
		if (_material) {
			DestroyImmediate(_material);
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if (brownHole && brownHoleShader && material) {
			wtsp = cam.WorldToScreenPoint(brownHole.position);
			pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);
			material.SetVector("_HolePosition", pos);
			material.SetFloat("_Ratio", ratio);
			material.SetFloat("_Radius", radius);
			material.SetFloat("_Black", black);
			
			Graphics.Blit(src, dest, material);
		} else Graphics.Blit(src, dest);
	}
}
