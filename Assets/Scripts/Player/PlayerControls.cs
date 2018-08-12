using UnityEngine;
using System;
using UnityEngine.Events;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        [Serializable]
        public class TeleportEvent : UnityEvent<Vector3>
        {

        }

        [Serializable]
        public class TeleBallActiveEvent : UnityEvent<bool>
        {

        }

        public const int AIM_PHASE = 0;
        public const int TELE_PHASE = 1;
        public const int DEATH_PHASE = 2;

        public Rigidbody2D TeleBallPrefab;
        public Rigidbody2D TeleBallPredictorPrefab;
        public Transform PlayerExitPrefab;

        public TeleportEvent onTeleport;
        public TeleBallActiveEvent onBallActive;

        private int phase = AIM_PHASE;

        private ShootAtMouse shooter;
        private TeleportToThing teleporter;
        private Transform teleBallRef;
        private ChildAnimationController teleBallRefAnim;

        private Collision2D currentCollision;
        private bool teleBallPositionIsValid = false;
        private bool startAiming = false;

        private MetricTracker tracker;

        void Start()
        {
            shooter = GetComponentInChildren<ShootAtMouse>();
            teleporter = GetComponentInChildren<TeleportToThing>();
            tracker = Camera.main.GetComponent<MetricTracker>();
        }

        void Update()
        {
            switch (phase)
            {
                case AIM_PHASE:
                    if (startAiming && Input.GetMouseButton(0))
                    {
                        // TODO: initiate aim prediction
                        shooter.Shoot(TeleBallPredictorPrefab);
                        GetComponentInChildren<Animator>().Play("Aim");
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        startAiming = true;
                    }
                    else if (Input.GetMouseButtonUp(0) && startAiming)
                    {
                        startAiming = false;
                        // TODO: shoot at mouse
                        foreach (var predictor in GameObject.FindGameObjectsWithTag("TelePredictor")) Destroy(predictor.gameObject);

                        shooter.Shoot(TeleBallPrefab, true);
                        GetComponentInChildren<Animator>().Play("Throw");

                        phase = TELE_PHASE;
                        shooter.canShoot = false;
                        teleporter.canTeleport = true;
                    }
                    break;
                case TELE_PHASE:
                    if (teleBallRef == null || !teleBallRef.gameObject.activeSelf)
                    {
                        phase = AIM_PHASE;
                        teleBallRef = null;
                        teleBallRefAnim = null;
                        shooter.canShoot = true;
                        teleBallPositionIsValid = false;
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        // TODO: allow physics to proceed?
                        if (teleBallRef)
                        {
                            teleBallRefAnim.SetOn(false);
                        }

                    }
                    else
                    {
                        if (teleBallRef)
                        {
                            teleBallRefAnim.SetOn(true);
                        }
                        if (teleBallPositionIsValid)
                        {
                            // TODO: teleport once position on teleBall is valid
                            TeleportToBall();
                        }
                    }
                    break;

            }
        }

        public void Die() {
            phase = DEATH_PHASE;
            GetComponentInChildren<Animator>().Play("Death");
        }

        public void OnShoot(Transform shot)
        {

            teleBallRef = shot;
            teleBallRefAnim = teleBallRef.GetComponent<ChildAnimationController>();
            var collideAlert = teleBallRef.GetComponent<CollideAlert>();
            if (collideAlert != null)
            {
                collideAlert.OnCollideEnter.AddListener(TeleBallCollidedWithSomething);
                collideAlert.OnCollideExit.AddListener(TeleBallStoppedCollidingWithSomething);
            }

        }

        private void TeleBallCollidedWithSomething(Collision2D other)
        {
            currentCollision = other;

            // Do not collide with bad/corrupted tiles.
            if (other.gameObject.tag == Tags.BadTiles)
            {
                teleBallPositionIsValid = false;
            }
            else
            {
                teleBallPositionIsValid = true;
            }
        }

        private void TeleBallStoppedCollidingWithSomething(Collision2D other)
        {
            teleBallPositionIsValid = false;
        }

        private void TeleportToBall()
        {
            if (teleBallRef != null)
            {
                tracker.AddToTracking(MetricTracker.TELEPORT);
                Transform exit = Instantiate(PlayerExitPrefab);
                exit.position = transform.position;
                exit.rotation = transform.rotation;
                exit.GetComponentInChildren<Animator>().Play("TeleportOut");

                var adjustment = new Vector3(0, 0, 0);
                // TODO: this might act weird if there is more than 1 collision...
                if (currentCollision != null && currentCollision.contactCount > 0)
                {
                    var contactPoint = currentCollision.contacts[0];
                    var v2 = contactPoint.normal * -contactPoint.separation;
                    adjustment.x = v2.x;
                    adjustment.y = v2.y;

                    teleBallRef.transform.rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
                }

                teleBallRef.transform.position = teleBallRef.position + adjustment;

                teleporter.Teleport(teleBallRef);
                onTeleport.Invoke(teleBallRef.position);
                teleBallRef.GetComponent<SpawnOnDestroy>().shouldSpawn = false;
                Destroy(teleBallRef.gameObject);
                teleBallRef = null;
                shooter.canShoot = true;
                teleBallPositionIsValid = false;


                GetComponentInChildren<Animator>().Play("TeleportIn");
            }

            phase = AIM_PHASE;
        }
    }
}