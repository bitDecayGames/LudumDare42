using UnityEngine;

public class EQController : MonoBehaviour {

	public RectTransform MusicList;
	public RectTransform FXList;

	public PlayMusicButton PlayMusicButtonPrefab;
	public PlayFXButton PlayFxButtonPrefab;
	
	private string[] MusicNames = {
		"event:/Music/ArcadeTheme/MainThemeAction",
		"event:/Music/CreditsSong",
		"event:/Music/HypeSong",
		"event:/Music/MainTheme",
		"event:/Music/SpaceAmbience",
		"event:/Music/TitleScreen"
	};
	
	private FXStruct[] FXNames = {
		new FXStruct("event:/SFX/Ball/Beep/Beep", false),
		new FXStruct("event:/SFX/Ball/Beep/BeepLong", true),
		new FXStruct("event:/SFX/Ball/Landings/Dirt", false),
		new FXStruct("event:/SFX/Ball/Landings/Metal", false),
		new FXStruct("event:/SFX/Ball/Landings/Squish", false),
		new FXStruct("event:/SFX/Belt/BeltTighten", false),
		new FXStruct("event:/SFX/ButtonBreak/ButtonBreak", false),
		new FXStruct("event:/SFX/Crystal/Crystal", false),
		new FXStruct("event:/SFX/Explosions/CrystalExplosion", false),
		new FXStruct("event:/SFX/Hatch/CloseHatch", false),
		new FXStruct("event:/SFX/Hatch/OpenHatch", false),
		new FXStruct("event:/SFX/HelmetApplication/HelmetApplication", false),
		new FXStruct("event:/SFX/PickUpBall/PickUpBall", false),
		new FXStruct("event:/SFX/Player/Throw", false),
		new FXStruct("event:/SFX/Teleport/Teleport", false)
	};
	
	void Start () {
		foreach (var musicName in MusicNames) {
			var btn = Instantiate(PlayMusicButtonPrefab, MusicList);
			btn.MusicName = musicName;
			btn.Init();
		}

		foreach (var fxName in FXNames) {
			var btn = Instantiate(PlayFxButtonPrefab, FXList);
			btn.FXName = fxName.fxName;
			btn.IsLooped = fxName.isLooping;
			btn.Init();
		}
	}

	private struct FXStruct {
		public string fxName;
		public bool isLooping;

		public FXStruct(string fxName, bool isLooping) {
			this.fxName = fxName;
			this.isLooping = isLooping;
		}
	}
}
