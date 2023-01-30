using System;
using System.Collections;
using UnityEngine;

namespace Vitals
{
    public class ShieldController : VitalsController
    {
        private HealthController _healthController;
        private ArmorController _armorController;
        
        public bool HasShieldGenerator { get; set; }

        public Action onRestartShieldRegenerationDelay;

        protected override void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _armorController = GetComponent<ArmorController>();
            base.Awake();
        }

        private void OnEnable() => onRestartShieldRegenerationDelay += RestartRegenCountdown;

        private void OnDisable() => onRestartShieldRegenerationDelay -= RestartRegenCountdown;

        public override void UpdateValue(float value)
        {
            if (!QueryColliderIsEnabled() && value < 0)
            {
                Debug.LogWarning("Damage reported to shields, but shields are not active.");
                return;
            }

            void HandleShieldGain()
            {
                currentValue += value;
                onToggleCollider?.Invoke(true);
                _armorController.onToggleCollider?.Invoke(false);
                _healthController.onToggleCollider?.Invoke(false);
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
                    HandleShieldGain();
                }
            }
            else
            {
                HandleShieldGain();
            }
            
            onUpdateDisplay?.Invoke();
        }
        
        protected override IEnumerator RegenDelay()
        {
            if (!HasShieldGenerator) yield return null;

            yield return base.RegenDelay();
        }
    }
}