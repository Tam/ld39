using UnityEngine;

public class BayDoorAnimationManager : MonoBehaviour
{

	public void OnStart()
	{
		GameManger.sm.StartBayDoor();
	}

	public void OnEnd()
	{
		GameManger.sm.StopBayDoor();
	}
	
}
