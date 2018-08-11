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
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                var dir = mousePos - transform.position;
                var magnitude = Mathf.Clamp(dir.magnitude * speed, minSpeed, maxSpeed);
                var normDir = dir;
                normDir.Normalize();
                thing.velocity = normDir * magnitude;

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