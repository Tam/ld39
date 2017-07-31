using UnityEngine;

namespace Interactable
{
	public class GantryDown : Interactable
	{

		private static bool _hasLowered;
		
		private static bool _isAnimating;
		public static bool IsAnimating
		{
			get { return _isAnimating; }
			set
			{
				_isAnimating = value;
				if (value) GameManger.sm.StartGantryDown();
				else GameManger.sm.StopGantryDown();
			}
		}

		private void Start()
		{
			_highlightBatteries = true;
			_battery = GetComponentInChildren<Battery>();
		}

		public override void OnStartInteract()
		{
			//
		}

		public override void OnEndInteract()
		{
			//
		}

		private void FixedUpdate()
		{
			if (!_isInteracting || !Input.GetButton("Interact")) return;
			
			if (IsAnimating) return;
			
			if (_hasLowered)
			{
				_hasLowered = false;
				IsAnimating = true;
				_battery.BatteryLevel = 0f;
				_player.PlayerPowerLevel += 0.3f;
				_player.gm.am.RaiseGantry();
			}
			else
			{	
				if (_player.PlayerPowerLevel >= 0.3f)
				{
					_hasLowered = true;
					IsAnimating = true;
					_battery.BatteryLevel = 1f;
					_player.PlayerPowerLevel -= 0.3f;
					_player.gm.am.LowerGantry();
				}
			}
		}
	}
}
