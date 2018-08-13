using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splash {
	public class SplashGoToGameScene : MonoBehaviour
	{
		public FMOD.Studio.EventInstance titleScreenMusic;
		
		private void Start()
		{
			titleScreenMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TitleScreen");
			titleScreenMusic.start();
			DontDestroyOnLoad(gameObject);
		}

		public void StartSplashScreenEvent() {
			Debug.Log("start splash screen music here");
		}

		public void TightenBeltEvent() {
			Debug.Log("Tighten belt noise here and possibly clothes ruffle?");
			FMOD.Studio.EventInstance beltTightenEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Belt/BeltTighten");
			beltTightenEvent.start();
			beltTightenEvent.release();
		}

		public void BitDecayGamesImageEvent() {
			Debug.Log("BitDecayGames logo is just now being shown");
		}

		public void HelmetStartSlidingOntoHeadEvent() {
			Debug.Log("Shhhhh noise here");
			FMOD.Studio.EventInstance helmetEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/HelmetApplication/HelmetApplication");
			helmetEvent.start();
			helmetEvent.release();
		}

		public void HelmetFitOnToHeadFullyEvent() {
			Debug.Log("Loud pop noise lol");
		}

		public void LudumDare42ImageEvent() {
			Debug.Log("LDJame is just now being shown");
		}

		public void ThinkingAboutPickingUpBallEvent() {
			Debug.Log("...");
		}

		public void StartReachingForBallEvent() {
			Debug.Log("Maybe start messing with filters on music here?");
		}

		public void HandOnBallEvent() {
			Debug.Log("Maybe some sort of activation beeping?");
//			FMOD.Studio.EventInstance helmetEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/PickUpBall/PickUpBall");
//			helmetEvent.start();
//			helmetEvent.release();
		}

		public void ThinkingBeforeGameStartsEvent() {
			Debug.Log("Can't think of anything to put here");
		}
		
		public void GoToGameScreenEvent() {
			Debug.Log("Go to the game screen here");
			SceneManager.LoadScene("TitleScreen");
		}
	}
}
