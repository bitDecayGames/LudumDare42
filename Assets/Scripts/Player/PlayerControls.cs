using UnityEngine;

namespace Player {
    public class PlayerControls : MonoBehaviour {

        public const int AIM_PHASE = 0;
        public const int TELE_PHASE = 1;

        public Rigidbody2D TeleBallPrefab;
        
        private int phase = AIM_PHASE;

        private ShootAtMouse shooter;
        private TeleportToThing teleporter;
        private Transform teleBallRef;
        
        void Start() {
            shooter = GetComponent<ShootAtMouse>();
            teleporter = GetComponent<TeleportToThing>();
        }

        void Update() {
            switch (phase) {
                case AIM_PHASE:
                    if (Input.GetMouseButtonDown(0)) {
                        // TODO: initiate aim prediction
                    } else if (Input.GetMouseButtonUp(0)) {
                        // TODO: shoot at mouse
                        shooter.Shoot(TeleBallPrefab);
                        phase = TELE_PHASE;
                        shooter.canShoot = false;
                        teleporter.canTeleport = true;
                    }
                    break;
                case TELE_PHASE:
                    if (Input.GetMouseButton(0)) {
                        // TODO: allow physics to proceed?
                        
                        // TODO: remove this
                        TeleportToBall();                     
                    } else {
                        // TODO: teleport once position on teleBall is valid
                        
                    }
                    break;
            }
        }

        public void OnShoot(Transform shot) {
            Debug.Log("Got shot!");
            teleBallRef = shot;
        }

        private void TeleportToBall() {
            if (teleBallRef != null) {
                teleporter.Teleport(teleBallRef);
                Destroy(teleBallRef.gameObject);
                teleBallRef = null;
                shooter.canShoot = true;
            }

            phase = AIM_PHASE;
        }
    }
}