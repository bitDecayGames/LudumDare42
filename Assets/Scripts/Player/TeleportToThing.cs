using UnityEngine;

namespace Player
{
    public class TeleportToThing : MonoBehaviour
    {

        public bool canTeleport = false;

        public void Teleport(Transform thing)
        {
            if (canTeleport)
            {
                transform.position = thing.position;
                transform.rotation = thing.rotation;
                canTeleport = false;
            }
        }
    }
}
