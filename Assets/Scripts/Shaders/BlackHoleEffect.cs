using UnityEngine;

public class BlackHoleEffect : MonoBehaviour {

	public Shader blackHoleShader;
	public Transform blackHole;
	[Range(-3f, .4f)]
	public float radius;
	[Range(0.1f, 3f)]
	public float distance;
	[Range(0, 20)]
	public float black;

	[HideInInspector]
	public float ratio;

	private Camera cam;
	private Material _material;
	private Vector3 wtsp;
	private Vector2 pos;

	public Material material {
		get {
			if (_material == null) {
				_material = new Material(blackHoleShader);
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
		if (blackHole && blackHoleShader && material) {
			wtsp = cam.WorldToScreenPoint(blackHole.position);
			pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);
			material.SetVector("_Position", pos);
			material.SetFloat("_Ratio", ratio);
			material.SetFloat("_Radius", radius);
			material.SetFloat("_Black", black);
			material.SetFloat("_Distance", distance);
			
			Graphics.Blit(src, dest, material);
		} else Graphics.Blit(src, dest);
	}
}
