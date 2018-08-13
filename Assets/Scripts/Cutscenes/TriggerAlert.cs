using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAlert : MonoBehaviour {
    [Serializable]
    public class OnTriggerEvent : UnityEvent<Collider2D> {}
    
    public OnTriggerEvent OnCollideEnter = new OnTriggerEvent();
    public OnTriggerEvent OnCollideExit = new OnTriggerEvent();
    
    void OnTriggerEnter2D(Collider2D other) {
        OnCollideEnter.Invoke(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        OnCollideExit.Invoke(other);
    }
}

