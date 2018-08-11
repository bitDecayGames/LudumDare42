using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class ShootAtMouse : MonoBehaviour {
        [Serializable]
        public class OnShootEvent : UnityEvent<Transform> { }

        public float speed;
        public float minSpeed;
        public float maxSpeed;
        public bool canShoot = true;
        public OnShootEvent onShoot = new OnShootEvent();

        public void Shoot(Rigidbody2D thingToShoot) {
            if (canShoot) {
                Debug.Log("Shoot");
                var thing = Instantiate(thingToShoot);
                thing.gameObject.SetActive(true);
                thing.transform.position = transform.position;
                var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                thing.velocity = dir.normalized * Mathf.Max(Mathf.Min(dir.magnitude * speed, maxSpeed), minSpeed);

                onShoot.Invoke(thing.transform);
            }
        }
    }

   
}