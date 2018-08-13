﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class BlinkController : MonoBehaviour
{

    private DestroyAfterTimeLimit timer;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        timer = GetComponent<DestroyAfterTimeLimit>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timeLeft() < .75f)
        {
            anim.Play("LightOnTeleBall");
        }
        else if (timer.timeLeft() < 2)
        {
            anim.Play("FastBlinkTeleball");
        }
        else
        {
            anim.Play("SlowBlinkTeleBall");
        }
    }

    public void blinkOccurred()
    {
        // TODO: play beep sound effect
    }
}
