using TMPro;
using UnityEngine;

namespace Health
{
    public class HealthDisplayUI : MonoBehaviour
    {
        [SerializeField] private HealthController healthController;
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            if (_text == null)
                _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable() => healthController.onUpdateHealthDisplay += UpdateHealthDisplay;

        private void OnDisable() => healthController.onUpdateHealthDisplay -= UpdateHealthDisplay;

        public void UpdateHealthDisplay() => _text.text = healthController.GetHealth().ToString();
    }
}