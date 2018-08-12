using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWarpShader : MonoBehaviour
{

    public Shader shader;
    public Material mat;

    public Camera cam;
    private Vector3 wtsp;

    // Use this for initialization
    void Start()
    {
        mat = new Material(shader);
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (shader && mat)
        {
            wtsp = cam.WorldToScreenPoint(new Vector3());
        }
    }
}
