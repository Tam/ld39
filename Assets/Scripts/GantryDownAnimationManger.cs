using Interactable;
using UnityEngine;

public class GantryDownAnimationManger : MonoBehaviour
{
	public GameObject Ramp;

	private void Start()
	{
		Ramp.SetActive(false);
	}

	public void AnimationDownEnd()
	{
		Ramp.SetActive(true);
		GantryDown.IsAnimating = false;
	}
	
	public void AnimationUpEnd()
	{
		Ramp.SetActive(false);
		GantryDown.IsAnimating = false;
	}
}
