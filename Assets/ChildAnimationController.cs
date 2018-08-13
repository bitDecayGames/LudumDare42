using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimationController : MonoBehaviour
{

    public string OnAnimation;
    public string OffAnimation;
    public bool play;

    private bool previous;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        previous = !play;
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (play != previous)
        {
            if (play)
            {
                anim.Play(OnAnimation);
            }
            else
            {
                anim.Play(OffAnimation);
            }

            previous = play;
        }
    }

    public void SetOn(bool active)
    {
        play = active;
    }
}
