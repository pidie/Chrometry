using Player;
using UnityEngine;

namespace Data.Scripts
{
    // static methods that govern how items work. One item can call multiple methods if need be
    public class ItemFunctions : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float minHealthPercentageHealed;
        [SerializeField] private float maxHealthPercentageHealed;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GameObject.Find("*** ACTORS").GetComponentInChildren<PlayerController>();
        }

        public void IncreaseHealth()
        {
            var healthController = _playerController.GetComponent<Health.HealthController>();
            var maxHealth = healthController.GetMaxHealth();
            var currentHealth = healthController.GetCurrentHealth();
            var amountToHeal = 0f;

            if (currentHealth >= maxHealth * 0.7f)
                amountToHeal = maxHealth * minHealthPercentageHealed * 0.01f;
            else
            {
                amountToHeal = Mathf.Lerp(maxHealthPercentageHealed, minHealthPercentageHealed,
                    currentHealth / (maxHealth * 0.7f));
            }

            healthController.UpdateHealth(amountToHeal);
            print($"Player gained {amountToHeal} health.");
        }

        public void AddArmor()
        {
            
        }
    }
}