using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    public float ScrollSpeed;
    public float EndTime;
    public float timePassed;

    private Vector3 pos;

    // Use this for initialization
    void Start()
    {
        pos = new Vector3();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        pos.Set(transform.position.x, transform.position.y, transform.position.z);
        pos.y += ScrollSpeed * Time.deltaTime;
        transform.position = new Vector3(pos.x, pos.y, pos.z);

        if (timePassed >= EndTime)
        {
//            Debug.Log("DONE");
        }
    }
}
