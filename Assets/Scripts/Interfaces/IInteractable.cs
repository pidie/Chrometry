using UnityEngine.InputSystem;

namespace Interfaces
{
	public interface IInteractable
	{
		void Interact(Player.PlayerController playerController, InputAction.CallbackContext ctx);

		void DisplayAlert();
	}
}