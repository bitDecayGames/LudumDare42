using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWarpShader : MonoBehaviour
{

    public Shader shader;
    private Material _material;

    public Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.DontSave;
            }
            return _material;
        }
    }

    private Camera cam;
    [HideInInspector]
    public float ratio;
    private Vector3 wtsp;

    public Vector3 warpPoint;
    public float radius;
    [Range(0, 0.3f)]
    public float waveThickness;
    [Range(0, 0.05f)]
    public float waveSpeed;
    [Range(1, 6)]
    public float distortion;
    private Vector2 pos;


    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        ratio = 1f / cam.aspect;
    }

    private void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }

    void Update()
    {
        radius += waveSpeed;
    }

    public void setWarpPosition(Vector3 vec)
    {
        warpPoint = vec;
        radius = 0;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (shader && material)
        {
            wtsp = cam.WorldToScreenPoint(warpPoint);
            pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);
            material.SetVector("_WarpOrigin", pos);
            // material.SetVector("_WarpOrigin", new Vector2(.1f, .1f));
            material.SetFloat("_Radius", radius);
            material.SetFloat("_Ratio", ratio);
            material.SetFloat("_Distortion", distortion);
            material.SetFloat("_WaveThickness", waveThickness);

            Graphics.Blit(src, dest, material);
        }
    }
}
