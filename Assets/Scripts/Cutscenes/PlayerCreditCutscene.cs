using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerCreditCutscene : MonoBehaviour
{

    [Serializable]
    public class TeleportEvent : UnityEvent<Vector3>
    {

    }
    public TeleportEvent onTeleport;


    public float spinSpeed;
    public float driftSpeed;

    private Vector3 Direction;

    // Use this for initialization
    void Start()
    {
        bool cameFromTitle = true;
        
        GameObject creditData = GameObject.Find("CreditData(Clone)");
        if (creditData)
        {
            cameFromTitle = creditData.GetComponent<CreditMetadata>().cameFromTitle;
            Destroy(creditData);
        }

        if (cameFromTitle)
        {
            // TODO: Play other music(?)
            var creditsMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/ClickCredits");
            creditsMusic.start();
            creditsMusic.release();
            Destroy(transform.parent.gameObject);
        }
        else
        {
            
        }

        Direction = new Vector3(-1, -3, 0);

        Direction = Direction.normalized;
        GetComponent<Animator>().Play("CreditSpin");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.fwd * Time.deltaTime * spinSpeed);
        transform.parent.transform.Translate(Direction * driftSpeed);
    }

    public void WarpIn()
    {
        // TODO: shader effect here
        // TODO: warp in sound here
        onTeleport.Invoke(transform.position);
    }

    public void beep()
    {
        // TODO: beep sound here
    }
}
