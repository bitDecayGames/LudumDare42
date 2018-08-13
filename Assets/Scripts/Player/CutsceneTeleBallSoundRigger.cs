using FMODUnity;
using UnityEngine;

public class CutsceneTeleBallSoundRigger : MonoBehaviour {

	public void HitSomething(Collision2D other) {

		if (other.collider.bounciness <= 0)
		{
			RuntimeManager.PlayOneShot("event:/SFX/Ball/Landings/Squish");
		}
		else if (other.collider.bounciness >= 1)
		{
			RuntimeManager.PlayOneShot("event:/SFX/Ball/Landings/Metal");
		}
		else
		{
			RuntimeManager.PlayOneShot("event:/SFX/Ball/Landings/Dirt");
		}
	}
}
