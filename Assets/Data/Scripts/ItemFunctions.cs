using UnityEngine;

namespace Data.Scripts
{
    // static methods that govern how items work. One item can call multiple methods if need be
    public class ItemFunctions : MonoBehaviour
    {
        [SerializeField, Tooltip("Percentage of max health healed")] private float healthItemHealAmount;
        
        private static float _healthPercentage;

        private void Awake() => _healthPercentage = healthItemHealAmount / 100f;
        
        public static void IncreaseHealth(Health.HealthController healthController)
        {
            healthController.UpdateHealth(healthController.GetMaxHealth() * _healthPercentage);
        }
    }
}