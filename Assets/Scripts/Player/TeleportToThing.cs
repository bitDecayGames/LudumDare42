using UnityEngine;

namespace Player
{
    public class TeleportToThing : MonoBehaviour
    {
        private MainGameSFXController _sfxController;
        
        private void Start()
        {
            _sfxController = GameObject.Find("MainGameSFXController").GetComponent<MainGameSFXController>();
        }

        public bool canTeleport = false;

        public void Teleport(Transform thing)
        {
            if (canTeleport)
            {
                _sfxController.PlayTeleportSound();
                
                transform.position = thing.position;
                transform.rotation = thing.rotation;
                canTeleport = false;
            }
        }
    }
}
