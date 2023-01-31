using System;
using System.Collections;
using UnityEngine;

namespace Vitals
{
    public class ShieldController : VitalsController
    {
        private HealthController _healthController;
        private ArmorController _armorController;
        private EnergyController _energyController;
        
        public bool HasShieldGenerator { get; set; }

        public Action onRestartShieldRegenerationDelay;

        protected override void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _armorController = GetComponent<ArmorController>();
            _energyController = GetComponent<EnergyController>();
            base.Awake();
        }

        private void OnEnable() => onRestartShieldRegenerationDelay += RestartRegenCountdown;

        private void OnDisable() => onRestartShieldRegenerationDelay -= RestartRegenCountdown;
        
        protected override void Update()
        {
            if (currentValue > maxValue) currentValue = maxValue;
            if (currentValue < maxValue && canRegen)
            {
                // currentValue += vitalRegen * Time.deltaTime;
                _energyController.onUseCharge.Invoke(vitalRegen * Time.deltaTime);
                onToggleCollider?.Invoke(true);
                onUpdateDisplay?.Invoke();
            }
        }

        public override void UpdateValue(float value)
        {
            if (!QueryColliderIsEnabled() && value < 0)
            {
                Debug.LogWarning("Damage reported to shields, but shields are not active.");
                return;
            }

            if (value < 0)
            {
                RestartRegenCountdown();

                if (value + currentValue <= 0)
                {
                    currentValue = 0;
                    onToggleCollider?.Invoke(false);

                    if (_armorController.CurrentValue > 0)
                        _armorController.onToggleCollider?.Invoke(true);
                    else
                        _healthController.onToggleCollider?.Invoke(true);
                }
                else
                {
                    HandleShieldGainFromSource();
                }
            }
            else
            {
                HandleShieldGainFromSource();
            }
            
            onUpdateDisplay?.Invoke();
            
            // internal method
            void HandleShieldGainFromSource()
            {
                currentValue += value;
                onToggleCollider?.Invoke(true);
                _armorController.onToggleCollider?.Invoke(false);
                _healthController.onToggleCollider?.Invoke(false);
            }
        }
        
        protected override IEnumerator RegenDelay()
        {
            if (!HasShieldGenerator) yield return null;

            yield return base.RegenDelay();
        }
    }
}