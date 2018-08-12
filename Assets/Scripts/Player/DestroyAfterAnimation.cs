using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float delay = 0f;

    // Use this for initialization
    void Start()
    {
        Destroy(transform.parent.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}