using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMetadata : MonoBehaviour
{

    public bool cameFromTitle = true;

    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
