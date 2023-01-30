using Player;
using UnityEngine;
using Vitals;

namespace Data.Scripts
{
    // static methods that govern how items work. One item can call multiple methods if need be
    public class ItemFunctions : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float minHealthPercentageHealed;
        [SerializeField] private float maxHealthPercentageHealed;

        [Header("Armor")] 
        [SerializeField] private float armorAdded;

        [Header("Shield")] 
        [SerializeField] private float shieldAdded;

        private static PlayerController _playerController;
        private static float _minHealthPercentageHealed;
        private static float _maxHealthPercentageHealed;
        private static float _armorAdded;
        private static float _shieldAdded;

        private void Awake()
        {
            _playerController = GameObject.Find("*** ACTORS").GetComponentInChildren<PlayerController>();
            _minHealthPercentageHealed = minHealthPercentageHealed;
            _maxHealthPercentageHealed = maxHealthPercentageHealed;
            _armorAdded = armorAdded;
            _shieldAdded = shieldAdded;
        }

        public void IncreaseHealth()
        {
            var healthController = _playerController.GetComponent<HealthController>();
            var maxHealth = healthController.MaxValue;
            var currentHealth = healthController.CurrentValue;
            float amountToHeal;

            if (currentHealth >= maxHealth * 0.7f)
                amountToHeal = maxHealth * _minHealthPercentageHealed * 0.01f;
            else
            {
                amountToHeal = Mathf.Lerp(_maxHealthPercentageHealed, _minHealthPercentageHealed,
                    currentHealth / (maxHealth * 0.7f));
            }

            healthController.UpdateValue(amountToHeal);
            print($"Player gained {amountToHeal} health.");
        }

        public void AddArmor()
        {
            var armorController = _playerController.GetComponent<ArmorController>();
            armorController.UpdateValue(_armorAdded);
            
            print($"Player received {_armorAdded} armor.");
        }

        public void AddShield()
        {
            var shieldController = _playerController.GetComponent<ShieldController>();
            shieldController.UpdateValue(_shieldAdded);
            shieldController.HasShieldGenerator = true;
            
            print($"Player received a generator and {_shieldAdded} shield.");
        }
    }
}