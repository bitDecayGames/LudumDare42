using UnityEngine;
using System.Collections;
using FMODUnity;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float delay = 0f;
    public bool destroyParent = true;

    // Use this for initialization
    void Start() {
        StartCoroutine(TriggerDestroy());
    }
    
    IEnumerator TriggerDestroy() {
        yield return new WaitForSeconds(.001f);
        var obj = gameObject;
        if (destroyParent) obj = transform.parent.gameObject;
        var anim = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        Destroy(obj, anim.length + delay);
    }

    public void ExplosionNoise()
    {
        if (GameObject.Find("EventCutsceneTrigger") != null)
        {
            if (GameObject.Find("EventCutsceneTrigger").GetComponent<StartCutscene>().CutsceneIsPlaying)
            {
                return;
            }
        }

        RuntimeManager.PlayOneShot("event:/SFX/Ball/Explode/Explode");
    }
}