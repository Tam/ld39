using Interactable;
using UnityEngine;

public class PlatformAnimationManager : MonoBehaviour
{
	public void AnimationEnd()
	{
		Platform.IsAnimating = false;
	}

}
