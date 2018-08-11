using System;
using UnityEngine;
public class KeyboardController : MonoBehaviour
{
    private float _speedMultiplier = 5;
    public GameObject BlackHole;

    private void Start()
    {
        if (BlackHole == null)
        {
            throw new Exception("BlackHole game object not assigned in editor");
        }
    }

    void Update()
    {
        if (Math.Abs(Input.GetAxis("Horizontal")) > .001)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _speedMultiplier * Time.deltaTime);
        }
        if (Math.Abs(Input.GetAxis("Vertical")) > .001)
        {
            transform.Translate(Vector3.up * Input.GetAxis("Vertical") * _speedMultiplier * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var blackHoleController = gameObject.AddComponent<GetSuckedIntoBlackHoleController>();
        blackHoleController.SetBlackHole(BlackHole);
        Destroy(this);
    }
}