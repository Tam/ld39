using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public AudioSource PlatformSource;
	public AudioSource PlatformIn;
	public AudioSource PlatformOut;
	
	public AudioSource GantryMoveSource;
	public AudioSource GantryMoveIn;
	public AudioSource GantryMoveOut;
	
	public AudioSource GantryDownSource;
	public AudioSource GantryDownIn;
	public AudioSource GantryDownOut;
	
	public AudioSource BayDoorSource;
	public AudioSource BayDoorIn;
	public AudioSource BayDoorOut;

	public void StartPlatform()
	{
		StartCoroutine(FadeIn(PlatformIn, 0.1f));
		StartCoroutine(FadeIn(PlatformSource, 0.1f));
	}

	public void StopPlatform()
	{
		StartCoroutine(FadeIn(PlatformOut, 0.1f));
		StartCoroutine(FadeOut(PlatformSource, 0.1f));
	}

	public void StartGantryMove()
	{
		StartCoroutine(FadeIn(GantryMoveIn, 0.1f));
		StartCoroutine(FadeIn(GantryMoveSource, 0.1f));
	}

	public void StopGantryMove()
	{
		StartCoroutine(FadeIn(GantryMoveOut, 0.1f));
		StartCoroutine(FadeOut(GantryMoveSource, 0.1f));
	}

	public void StartGantryDown()
	{
		StartCoroutine(FadeIn(GantryDownIn, 0.1f));
		StartCoroutine(FadeIn(GantryDownSource, 0.1f));
	}

	public void StopGantryDown()
	{
		StartCoroutine(FadeIn(GantryDownOut, 0.1f));
		StartCoroutine(FadeOut(GantryDownSource, 0.1f));
	}

	public void StartBayDoor()
	{
		StartCoroutine(FadeIn(BayDoorIn, 0.1f, 0.2f));
		StartCoroutine(FadeIn(BayDoorSource, 0.1f, 0.3f));
	}

	public void StopBayDoor()
	{
		StartCoroutine(FadeIn(BayDoorOut, 0.1f, 0.2f));
		StartCoroutine(FadeOut(BayDoorSource, 0.1f));
	}

	// Helpers
	// =========================================================================

	public static IEnumerator FadeIn(
		AudioSource audioSource, float FadeTime, float maxVol = 1.0f
	) {
		float startVolume = 0.1f;
 
		audioSource.volume = 0;
		audioSource.Play();
 
		while (audioSource.volume < maxVol)
		{
			audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
			yield return null;
		}
 
		audioSource.volume = 1f;
	}

	public static IEnumerator FadeOut(
		AudioSource audioSource, float duration, bool stop = true
	) {
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / duration;
			yield return null;
		}

		if (stop)
		{
			audioSource.Stop();
			audioSource.volume = startVolume;
		}
	}
	
}
