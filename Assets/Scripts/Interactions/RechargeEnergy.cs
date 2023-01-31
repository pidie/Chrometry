using System;
using Interfaces;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
    public class RechargeEnergy : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject alertPrefab;
        [SerializeField] private float transferRate;
        [SerializeField] private string messageText;

        private GameObject _alert;
        private TMP_Text _message;

        private void OnTriggerEnter(Collider other)
        {
            var trigger = other.GetComponent<InteractableTrigger>();
            
            if (trigger)
            {
                var pc = trigger.PlayerController;
                pc.IsInRangeOfInteractable = true;
                pc.InteractablesInRange.Add(this);

                if ((RechargeEnergy)pc.InteractablesInRange[0] == this)
                    DisplayAlert();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var trigger = other.GetComponent<InteractableTrigger>();
            
            if (trigger)
            {
                var pc = trigger.PlayerController;
                pc.InteractablesInRange.Remove(this);
                Destroy(_alert);
                
                switch (pc.InteractablesInRange.Count)
                {
                    case 0:
                        pc.IsInRangeOfInteractable = false;
                        break;
                    case > 0:
                        pc.InteractablesInRange[0].DisplayAlert();
                        break;
                }
            }
        }

        public void Interact(PlayerController playerController, InputAction.CallbackContext ctx)
        {
            var energyController = playerController.GetComponent<Vitals.EnergyController>();
            if (ctx.performed)
                energyController.onStartCharging?.Invoke(transferRate);
            else if (ctx.canceled)
                energyController.onStopCharging?.Invoke();
        }

        public void DisplayAlert()
        {
            UserInterfaceManager.onCreateCanvas.Invoke(alertPrefab);
            _alert = UserInterfaceManager.LastCreatedCanvas;
            _message = _alert.GetComponentInChildren<TMP_Text>();
            _message.text = $"{messageText}";
        }
    }
}
