using System;
using System.Collections;
using UnityEngine;

namespace Health
{
    public class HealthController : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealthOverride;
        [SerializeField, Tooltip("Health regenerated per second")] private float healthRegen = 1.5f;
        [SerializeField, Tooltip("Delay after taking damage before health will regenerate")] private float healthRegenDelay = 2.5f;

        private float _currentHealth;
        private bool _canRegen;

        // controller scripts should have separate methods handling death that subscribe to onDeath
        public Action onDeath;
        public Action onUpdateHealthDisplay;
    
        public float GetCurrentHealth() => _currentHealth;

        public float GetMaxHealth() => maxHealth;

        public void SetMaxHealth(float value, bool updateCurrentHealth = true)
        {
            maxHealth = value;
        
            if (updateCurrentHealth)
                _currentHealth = maxHealth;
        }
    
        private void Awake()
        {
            _currentHealth = currentHealthOverride < maxHealth ? currentHealthOverride : maxHealth;
            onUpdateHealthDisplay?.Invoke();
        }

        private void Update()
        {
            if (_currentHealth > maxHealth) _currentHealth = maxHealth;
            if (_currentHealth < maxHealth && _canRegen)
            {
                _currentHealth += healthRegen * Time.deltaTime;
                onUpdateHealthDisplay?.Invoke();
            }
        }

        public void UpdateHealth(float value)
        {
            _currentHealth += value;
            if (value < 0)
            {
                _canRegen = false;
                StopAllCoroutines();
                StartCoroutine(HealthRegenDelay());

                if (_currentHealth < 0)
                    onDeath.Invoke();
            }
        
            onUpdateHealthDisplay?.Invoke();
        }

        public void UpdateMaxHealth(float value) => maxHealth += value;

        private IEnumerator HealthRegenDelay()
        {
            yield return new WaitForSeconds(healthRegenDelay);
            _canRegen = true;
        }
    }
}