using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthDisplayUI : MonoBehaviour
    {
        [SerializeField] private HealthController healthController;
        [SerializeField] private Color maxHealthColor;
        [SerializeField] private Color minHealthColor;
        
        private TMP_Text _text;
        private Image _iconPlayer;
        private Image _icon;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            if (_text == null)
                _text = GetComponentInChildren<TMP_Text>();

            if (healthController.GetComponent<PlayerController>())
                _iconPlayer = GetComponent<Image>();
            else
                _icon = GetComponent<Image>();
        }

        private void Start() => UpdateHealthDisplay();

        private void OnEnable()
        {
            healthController.onUpdateHealthDisplay += UpdateHealthDisplay;
            healthController.onUpdateHealthDisplay.Invoke();
        }

        private void OnDisable() => healthController.onUpdateHealthDisplay -= UpdateHealthDisplay;

        public void UpdateHealthDisplay()
        {
            if (_text != null)
                _text.text = healthController.GetCurrentHealth().ToString();
            
            if (_iconPlayer != null)
            {
                var maxHealth = healthController.GetMaxHealth();
                var twentyPercent = maxHealth * 0.2f;
                var eightyPercent = maxHealth * 0.8f;
                var currentHealth = healthController.GetCurrentHealth();

                if (currentHealth >= twentyPercent)
                {
                    var percentage = (currentHealth - twentyPercent) / eightyPercent;
                    _iconPlayer.fillAmount = 1f;
                    _iconPlayer.color = Color.Lerp(minHealthColor, maxHealthColor, percentage);
                }
                else
                    _iconPlayer.fillAmount = currentHealth / twentyPercent;
            }
        }
    }
}