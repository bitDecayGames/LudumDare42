using System;
using UnityEngine;
public class FollowMouseController : MonoBehaviour
{
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
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var blackHoleController = gameObject.AddComponent<GetSuckedIntoBlackHoleController>();
        blackHoleController.SetBlackHole(BlackHole);
        Destroy(this);
    }
}