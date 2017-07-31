using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	
	// Variables
	// =========================================================================
	// Variables: Animators
	// -------------------------------------------------------------------------
	public Animator BayDoorAnimator;
	public Animator BayDoorGuyAnimator;
	public Animator GantryAnimator;
	public Animator GantryDownAnimator;
	
	// Variables: Animation Keys
	// -------------------------------------------------------------------------
	private readonly int _bayDoorClose = Animator.StringToHash("CloseBayDoor");
	private readonly int _bayDoorGuyExit = Animator.StringToHash("Exit");
	private readonly int _move = Animator.StringToHash("Move");
	private readonly int _down = Animator.StringToHash("Down");
	
	// Animation Triggers
	// =========================================================================
	public void CloseBayDoor()
	{
		BayDoorAnimator.SetBool(_bayDoorClose, true);
	}

	public void ExitBayDoorGuy()
	{
		BayDoorGuyAnimator.SetTrigger(_bayDoorGuyExit);
	}

	public void MoveGantry()
	{
		GantryAnimator.SetTrigger(_move);
	}

	public void LowerGantry()
	{
		GantryDownAnimator.SetBool(_down, true);
	}

	public void RaiseGantry()
	{
		GantryDownAnimator.SetBool(_down, false);
	}
	
}
