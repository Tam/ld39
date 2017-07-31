using UnityEngine;

namespace Interactable
{
	public class Gantry : Interactable
	{
		private bool _hasRun;

		private void Start()
		{
			_highlightBatteries = true;
			_battery = GetComponentInChildren<Battery>();
		}

		private void FixedUpdate()
		{
			if (!_isInteracting || !Input.GetButton("Interact") || _hasRun) 
				return;

			if (_player.PlayerPowerLevel >= 0.3f)
			{
				_battery.BatteryLevel = 1f;
				_player.PlayerPowerLevel -= 0.3f;
				_highlightBatteries = false;
				_hasRun = true;
				_player.gm.am.MoveGantry();
			}
		}

		public override void OnStartInteract()
		{
			//
		}

		public override void OnEndInteract()
		{
			//
		}
	}
}
