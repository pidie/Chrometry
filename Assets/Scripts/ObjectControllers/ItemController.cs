using Data.Scripts;
using Interfaces;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ObjectControllers
{
    public class ItemController : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData data;
        [SerializeField] private Image icon;
        [SerializeField] private GameObject alertPrefab;

        private GameObject _alert;
        private TMP_Text _message;

        private void Awake() => icon.sprite = data.icon;

        private void OnTriggerEnter(Collider other)
        {
            // only if it's a player
            if (other.GetComponent<PlayerController>())
            {
                var pc = other.GetComponent<PlayerController>();
            
                // stop if the player has an item
                if (pc.isHoldingItem) return;
            
                // set the item as available for pickup
                pc.IsInRangeOfInteractable = true;
                pc.Interactable = this;
                UserInterfaceManager.onCreateCanvas.Invoke(alertPrefab);
                _alert = UserInterfaceManager.LastCreatedCanvas;
                _message = _alert.GetComponentInChildren<TMP_Text>();
                _message.text = $"{data.itemName}";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // only if it's a player
            if (other.GetComponent<PlayerController>())
            {
                // reverts the item as not available for pickup
                var pc = other.GetComponent<PlayerController>();
                pc.IsInRangeOfInteractable = false;
                pc.Interactable = default;
                Destroy(_alert);
            }
        }

        // destroy the pop up if the gameObject is destroyed
        private void OnDestroy()
        {
            if (_alert)
                Destroy(_alert);
        }

        // calls the item to be added to the player and destroys the gameObject
        public void Interact()
        {
            PlayerController.onAddItemToPlayer.Invoke(data);
            if (gameObject)
                Destroy(gameObject);   
        }
    }
}