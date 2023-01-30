using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class PlayerItemBox : MonoBehaviour
    {
        [SerializeField] private Sprite baseImage;

        private Image _icon;

        private void Awake() => _icon = GetComponent<Image>();

        private void OnEnable() => PlayerController.onDisplayItemInUI += DisplayItem;

        private void OnDisable() => PlayerController.onDisplayItemInUI -= DisplayItem;

        private void DisplayItem(Sprite icon = null)
        {
            if (icon == null)
            {
                _icon.sprite = baseImage;
                _icon.color = Color.clear;
            }
            else
            {
                _icon.sprite = icon;
                _icon.color = Color.white;
            }
        }
    }
}
