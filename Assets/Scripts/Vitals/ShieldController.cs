namespace Vitals
{
    public class ShieldController : VitalsController
    {
        private HealthController _healthController;
        private ArmorController _armorController;
        private float _startingShield;

        protected override void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _armorController = GetComponent<ArmorController>();
            base.Awake();
            currentValue = _startingShield;
        }

        public override void UpdateValue(float value)
        {
            if (value < 0)
                RestartRegenCountdown();
            
            if (value + currentValue < 0)
            {
                if (_armorController.QueryLastCollision() < vitalRegenDelay)
                    RestartRegenCountdown();
                
                if (value + _armorController.CurrentValue < 0)
                {
                    if (_healthController.QueryLastCollision() < vitalRegenDelay)
                        RestartRegenCountdown();
                    
                    _armorController.UpdateValue(0);
                    _healthController.onToggleCollider(true);
                    _healthController.UpdateValue(value + _armorController.CurrentValue + currentValue);
                }
                else
                {
                    _armorController.onToggleCollider(true);
                    _armorController.UpdateValue(value + currentValue);
                }
                
                onToggleCollider.Invoke(false);
                currentValue = 0;
            }
            else
            {
                currentValue += value;
                onToggleCollider.Invoke(true);
                _armorController.onToggleCollider(false);
                _healthController.onToggleCollider(false);
            }
            
            onUpdateDisplay?.Invoke();
        }

        public void SetStartingShield(float value) => _startingShield = value;
    }
}