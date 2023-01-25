using System;
using System.Collections;
using UnityEngine;

/* HEALTH
 * 
 * The Health system has three parts. This - the Health component - is the operational part, storing and updating all information.
 * It should be placed on the root object. There should only be one health component for each game unit.
 * 
 * The HealthColliderController detects objects that would alter data stored in the Health section. It should be placed anywhere collisions
 * will be detected. There can be multiple HealthColliderControllers for one Health, but there must always be at least one to enable interaction.
 * 
 * The optional HealthDisplayUI allows Health data to be shown in the UI. This is only important if the health of the unit needs to be visualized.
 */

namespace Health
{
    public class HealthController : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField, Tooltip("Health regenerated per second")] private float healthRegen = 1.5f;
        [SerializeField, Tooltip("Delay after taking damage before health will regenerate")] private float healthRegenDelay = 2.5f;

        private float _currentHealth;
        private bool _canRegen;

        // controller scripts should have separate methods handling death that subscribe to onDeath
        public Action onDeath;
        public Action onUpdateHealthDisplay;
    
        public float GetHealth() => _currentHealth;

        public float GetMaxHealth() => maxHealth;

        public void SetMaxHealth(float value, bool updateCurrentHealth = true)
        {
            maxHealth = value;
        
            if (updateCurrentHealth)
                _currentHealth = maxHealth;
        }
    
        private void Awake()
        {
            _currentHealth = maxHealth;
            onUpdateHealthDisplay.Invoke();
        }

        private void Update()
        {
            if (_currentHealth > maxHealth) _currentHealth = maxHealth;
            if (_currentHealth < maxHealth && _canRegen)
            {
                _currentHealth += healthRegen * Time.deltaTime;
                onUpdateHealthDisplay.Invoke();
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
            }
        
            onUpdateHealthDisplay.Invoke();
        }

        public void UpdateMaxHealth(float value) => maxHealth += value;

        private IEnumerator HealthRegenDelay()
        {
            yield return new WaitForSeconds(healthRegenDelay);
            _canRegen = true;
        }
    }
}