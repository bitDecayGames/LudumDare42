using UnityEngine.Events;
using UnityEngine;
using System;

public class OnDestroyCallEvent : MonoBehaviour {
    [Serializable]
    public class OnDestroyEvent : UnityEvent<Transform> {}
		
    public OnDestroyEvent OnDestroyed = new OnDestroyEvent();

    void OnDestroy() {
        OnDestroyed.Invoke(transform);
    }
}
