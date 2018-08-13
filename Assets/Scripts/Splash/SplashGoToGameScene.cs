using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splash {
	public class SplashGoToGameScene : MonoBehaviour
	{
		public void StartSplashScreenEvent() {
		}

		public void TightenBeltEvent() {
			FMOD.Studio.EventInstance beltTightenEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Belt/BeltTighten");
			beltTightenEvent.start();
			beltTightenEvent.release();
		}

		public void BitDecayGamesImageEvent() {
		}

		public void HelmetStartSlidingOntoHeadEvent() {
			FMOD.Studio.EventInstance helmetEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/HelmetApplication/HelmetApplication");
			helmetEvent.start();
			helmetEvent.release();
		}

		public void HelmetFitOnToHeadFullyEvent() {
		}

		public void LudumDare42ImageEvent() {
		}

		public void ThinkingAboutPickingUpBallEvent() {
		}

		public void StartReachingForBallEvent() {
		}

		public void HandOnBallEvent() {
		}

		public void ThinkingBeforeGameStartsEvent() {
		}
		
		public void GoToGameScreenEvent() {
			SceneManager.LoadScene("Mike");
		}
	}
}
