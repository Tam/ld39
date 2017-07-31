using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{

	public Player Player;
	public ScreenFader ScreenFader;
	public AnimationManager am;
	public static AudioManager sm;
	
	private bool _shouldRespawn;
	private bool _shouldTransition;

	private void Awake()
	{
		sm = GetComponent<AudioManager>();
		Player.gm = this;
	}
	
	// Player Death
	// =========================================================================
	
	public void OnPlayerDeath ()
	{
		_shouldRespawn = true;
		ScreenFader.FadeOut(2);
	}

	public void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	// Screen Fader
	// =========================================================================

	public void OnScreenFadeOutComplete()
	{
		if (_shouldRespawn) ReloadLevel();
		if (_shouldTransition)
			SceneManager.LoadScene("__CREDITS__", LoadSceneMode.Single);
	}

	public void OnScreenFadeInComplete()
	{
	}
	
	// Levels
	// =========================================================================

	public void EndLevel1()
	{
		ScreenFader.FadeOut();
		_shouldTransition = true;
	}
	
}
