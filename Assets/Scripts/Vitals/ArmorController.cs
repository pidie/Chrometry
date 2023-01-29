using UnityEngine;

namespace Vitals
{
    public class ArmorController : VitalsController
    {
        [SerializeField] private float damageReduction = 0.3f;
        
        private HealthController _healthController;
        private float _startingArmor;

        protected override void Awake()
        {
            _healthController = GetComponent<HealthController>();
            base.Awake();
            currentValue = _startingArmor;
        }

        public override void UpdateValue(float value)
        {
            var mitigatedValue = value * (1 - damageReduction);
            if (mitigatedValue + currentValue < 0)
            {
                _healthController.onToggleCollider(true);
                _healthController.UpdateValue(mitigatedValue + currentValue); // this grants the armor resistance to the health on the same attack
                onToggleCollider.Invoke(false);
                currentValue = 0;
            }
            else
            {
                currentValue += mitigatedValue;
                var shieldActive = GetComponent<ShieldController>().QueryColliderIsEnabled();
                print(shieldActive);
                onToggleCollider.Invoke(!shieldActive);
                _healthController.onToggleCollider(false);
            }
            
            onUpdateDisplay?.Invoke();
        }

        public void SetStartingArmor(float value) => _startingArmor = value;
    }
}