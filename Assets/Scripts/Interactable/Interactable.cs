namespace Interactable
{
	using UnityEngine;
	
	public abstract class Interactable : MonoBehaviour
	{

		protected bool _highlightBatteries;
		protected bool _isInteracting;
		protected Player _player;
		protected Battery _battery;
		
		public abstract void OnStartInteract();

		public abstract void OnEndInteract();

		public void StartInteract(Player player)
		{
			_player = player;
			_isInteracting = true;
			OnStartInteract();

			if (_highlightBatteries && _battery)
			{
				_battery.Highlight();
				_player.Battery.Highlight();
			}
		}

		public void EndInteract()
		{
			_isInteracting = false;
			OnEndInteract();
			StopBatteryHighlight();
		}

		/// <summary>
		/// Stops the batteries being highlighted
		/// </summary>
		/// <param name="preventHighlight">
		/// If true, the next time the player enters the trigger the batteries
		/// won't be highlighted.
		/// </param>
		protected void StopBatteryHighlight(bool preventHighlight = false)
		{
			if (preventHighlight)
				_highlightBatteries = false;
			
			if (_highlightBatteries && _battery)
			{
				_battery.StopHighlight();
				_player.Battery.StopHighlight();
			}
		}

	}
}
