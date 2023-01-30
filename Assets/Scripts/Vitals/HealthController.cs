using System;

namespace Vitals
{
    public class HealthController : VitalsController
    {
        private ShieldController _shieldController;
        
        public Action onDeath;

        protected override void Awake()
        {
            _shieldController = GetComponent<ShieldController>();
            base.Awake();
        }

        public override void UpdateValue(float value)
        {
            currentValue += value;
            
            if (value < 0)
            {
                if (_shieldController != null && _shieldController.HasShieldGenerator)
                    _shieldController.onRestartShieldRegenerationDelay?.Invoke();
                if (currentValue < 0)
                {
                    onDeath.Invoke();
                    return;
                }
                
                RestartRegenCountdown();
            }

            onUpdateDisplay?.Invoke();
        }
    }
}