using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class ShootAtMouse : MonoBehaviour
    {
        [Serializable]
        public class OnShootEvent : UnityEvent<Transform> { }

        public float speed;
        public float minSpeed;
        public float maxSpeed;
        public bool canShoot = true;
        public OnShootEvent onShoot = new OnShootEvent();

        public float coolDownTimer = 0.1f;
        private bool cooledDown;
        private float _currentCoolDown = 0.1f;

        void Start()
        {
            ResetCooldown();
        }


        void Update()
        {
            if (_currentCoolDown < 0) ResetCooldown();
            else if (!cooledDown) _currentCoolDown -= Time.deltaTime;
        }

        public void Shoot(Rigidbody2D thingToShoot, bool forced = false)
        {
            if (canShoot && (cooledDown || forced))
            {
                cooledDown = false;
                var thing = Instantiate(thingToShoot);
                thing.gameObject.SetActive(true);
                thing.transform.position = transform.position;
                var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                thing.velocity = dir.normalized * Mathf.Max(Mathf.Min(dir.magnitude * speed, maxSpeed), minSpeed);

                onShoot.Invoke(thing.transform);
            }
        }

        private void ResetCooldown()
        {
            _currentCoolDown = coolDownTimer;
            cooledDown = true;
        }
    }


}