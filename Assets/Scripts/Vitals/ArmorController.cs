using UnityEngine;

namespace Vitals
{
    public class ArmorController : VitalsController
    {
        [SerializeField] private float damagePercentageMitigated = 0.3f;
        
        private HealthController _healthController;
        private ShieldController _shieldController;
        private float _damagePercentageMitigatedInverse;

        protected override void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _shieldController = GetComponent<ShieldController>();
            EvaluateDamagePercentMitigatedInverse();
            base.Awake();
        }

        public override void UpdateValue(float value)
        {
            if (!QueryColliderIsEnabled() && value < 0) return;
            
            var shieldIsActive = _shieldController.QueryColliderIsEnabled();
            
            if (value > 0)
            {
                if (!shieldIsActive)
                    onToggleCollider.Invoke(true);

                currentValue += value;
            }
            else
            {
                if (_shieldController != null && _shieldController.HasShieldGenerator)
                    _shieldController.onRestartShieldRegenerationDelay?.Invoke();
                    
                var damageToHealth = MitigateDamage(value, out var hasLeftoverDamage);

                if (hasLeftoverDamage)
                {
                    _healthController.onToggleCollider.Invoke(true);
                    _healthController.UpdateValue(damageToHealth);
                    onToggleCollider.Invoke(false);
                }
                else
                {
                    _healthController.onToggleCollider(false);
                }
            }
            
            onUpdateDisplay?.Invoke();
        }

        public float MitigateDamage(float damage, out bool leftoverDamage)
        {
            var mitigatedDamage = damage * (1 - damagePercentageMitigated);
            if (currentValue + mitigatedDamage < 0)
            {
                leftoverDamage = true;
                var damagePassedToHealth = damage + currentValue * _damagePercentageMitigatedInverse;
                currentValue = 0;
                return damagePassedToHealth;
            }
            
            leftoverDamage = false;
            currentValue += mitigatedDamage;
            return 0;
        }

        private void EvaluateDamagePercentMitigatedInverse() => _damagePercentageMitigatedInverse = 1 / (1 - damagePercentageMitigated);
    }
}