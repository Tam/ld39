using UnityEngine;

public class GantryMoveAnimationManager : MonoBehaviour
{
	
	public void MoveStart()
	{
		GameManger.sm.StartGantryMove();
	}
	
	public void MoveStop()
	{
		GameManger.sm.StopGantryMove();
	}
	
}
