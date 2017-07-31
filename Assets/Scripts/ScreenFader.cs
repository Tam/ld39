using System;
using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{

	public GameManger gm;

	private Animator _animator;
	private readonly int _showOverlay = Animator.StringToHash("ShowOverlay");

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	// Triggers
	// =========================================================================

	public void FadeOut(float delay = 0)
	{
		StartCoroutine(AfterDelay(delay, () =>
		{
			_animator.SetBool(_showOverlay, true);
		}));
	}

	public void FadeIn()
	{
		_animator.SetBool(_showOverlay, false);
	}
	
	// Events
	// =========================================================================

	public void OnOverlayFadeInComplete()
	{
		if (gm != null)
			gm.OnScreenFadeOutComplete();
	}

	public void OnOverlayFadeOutComplete()
	{
		if (gm != null)
			gm.OnScreenFadeInComplete();
	}
	
	// Misc
	// =========================================================================

	private IEnumerator AfterDelay(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);

		action();
	}

}
