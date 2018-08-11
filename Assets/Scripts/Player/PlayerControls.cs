using UnityEngine;

namespace Player {
    public class PlayerControls : MonoBehaviour {

        public const int AIM_PHASE = 0;
        public const int TELE_PHASE = 1;

        public Rigidbody2D TeleBallPrefab;
        public Rigidbody2D TeleBallPredictorPrefab;
        
        private int phase = AIM_PHASE;

        private ShootAtMouse shooter;
        private TeleportToThing teleporter;
        private Transform teleBallRef;

        private Collision2D currentCollision;
        private bool teleBallPositionIsValid = false;
        
        void Start() {
            shooter = GetComponent<ShootAtMouse>();
            teleporter = GetComponent<TeleportToThing>();
        }

        void Update() {
            switch (phase) {
                case AIM_PHASE:
                    if (Input.GetMouseButton(0)) {
                        // TODO: initiate aim prediction
                        shooter.Shoot(TeleBallPredictorPrefab);
                    } else if (Input.GetMouseButtonUp(0)) {
                        // TODO: shoot at mouse
                        foreach (var predictor in GameObject.FindGameObjectsWithTag("TelePredictor")) Destroy(predictor.gameObject);
                        
                        shooter.Shoot(TeleBallPrefab, true);
                        phase = TELE_PHASE;
                        shooter.canShoot = false;
                        teleporter.canTeleport = true;
                    }
                    break;
                case TELE_PHASE:
                    if (Input.GetMouseButton(0)) {
                        // TODO: allow physics to proceed?
                            
                    } else if (teleBallPositionIsValid) {
                        // TODO: teleport once position on teleBall is valid
                        TeleportToBall();
                    }
                    break;
            }
        }
        
        public void OnShoot(Transform shot) {
            teleBallRef = shot;
            var collideAlert = teleBallRef.GetComponent<CollideAlert>();
            if (collideAlert != null) {
                collideAlert.OnCollideEnter.AddListener(TeleBallCollidedWithSomething);
                collideAlert.OnCollideExit.AddListener(TeleBallStoppedCollidingWithSomething);
            }
            
        }

        private void TeleBallCollidedWithSomething(Collision2D other) {
            currentCollision = other;
            teleBallPositionIsValid = true;
        }

        private void TeleBallStoppedCollidingWithSomething(Collision2D other) {
            teleBallPositionIsValid = false;
        }

        private void TeleportToBall() {
            if (teleBallRef != null) {
                var adjustment = new Vector3(0, 0, 0);
                // TODO: this might act weird if there is more than 1 collision...
                if (currentCollision != null && currentCollision.contactCount > 0) {
                    var contactPoint = currentCollision.contacts[0];
                    var v2 = contactPoint.normal * -contactPoint.separation;
                    adjustment.x = v2.x;
                    adjustment.y = v2.y;
                }

                teleBallRef.transform.position = teleBallRef.position + adjustment;
                teleporter.Teleport(teleBallRef);
                Destroy(teleBallRef.gameObject);
                teleBallRef = null;
                shooter.canShoot = true;
                teleBallPositionIsValid = false;
            }

            phase = AIM_PHASE;
        }
    }
}