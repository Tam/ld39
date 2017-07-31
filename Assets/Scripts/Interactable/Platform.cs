using System.Collections.Generic;

namespace Interactable
{
	using UnityEngine;
	
	public class Platform : Interactable
	{
		// Variables
		// =====================================================================
		
		private Animator _anim;
		private static List<Battery> _batteries = new List<Battery>();

		private static bool _hasLowered;

		private static bool _isAnimating;
		public static bool IsAnimating
		{
			get { return _isAnimating; }
			set
			{
				_isAnimating = value;
				if (value) GameManger.sm.StartPlatform();
				else GameManger.sm.StopPlatform();
			}
		}

		private readonly int _platformDown = Animator.StringToHash("PlatformDown");
		
		// Unity
		// =====================================================================

		private void Start()
		{
			_highlightBatteries = true;
			_battery = GetComponentInChildren<Battery>();
			_batteries.Add(_battery);
			if (transform.parent.name == "Platform")
			{
				_anim = transform.parent.GetComponent<Animator>();
			}
			else
			{
				_anim = transform.parent.Find("PlatformOffset").Find("Platform")
					.GetComponent<Animator>();
			}
		}

		private void FixedUpdate()
		{
			if (!_isInteracting || !Input.GetButton("Interact")) return;
			
			if (IsAnimating) return;
			
			if (_hasLowered)
			{
				SetBatteryLevel(0f);
				_player.PlayerPowerLevel += 0.3f;
				RaisePlatform();
			}
			else
			{
				if (_player.PlayerPowerLevel >= 0.3f)
				{
					SetBatteryLevel(1f);
					_player.PlayerPowerLevel -= 0.3f;
					LowerPlatform();
				}
			}
		}

		// Interactable
		// =====================================================================

		public override void OnStartInteract()
		{
			//
		}

		public override void OnEndInteract()
		{
			//
		}
		
		// Platform
		// =====================================================================
		
		public void LowerPlatform()
		{
			IsAnimating = true;
			_hasLowered = true;
			_anim.SetBool(_platformDown, true);
		}

		public void RaisePlatform()
		{
			IsAnimating = true;
			_hasLowered = false;
			_anim.SetBool(_platformDown, false);
		}
		
		// Helpers
		// =====================================================================

		private void SetBatteryLevel(float level)
		{
			foreach (Battery battery in _batteries)
			{
				if (battery != null)
					battery.BatteryLevel = level;
			}
		}
		
	}
}
