using System;
using UnityEngine;

namespace Vitals
{
    public class EnergyController : VitalsController
    {
        [SerializeField] private VitalsController device;
        [SerializeField] private float transferRate;

        private bool _isCurrentlyCharging;
        private float _chargeRate;

        public float TransferRate => transferRate;
        public bool CanRegen { get; set; }

        public Action<float> onUseCharge;
        public Action<float> onStartCharging;
        public Action onStopCharging;

        private void OnEnable()
        {
            onUseCharge += UseCharge;
            onStartCharging += StartCharging;
            onStopCharging += StopCharging;
        }

        private void OnDisable()
        {
            onUseCharge -= UseCharge;
            onStartCharging -= StartCharging;
            onStopCharging -= StopCharging;
        }

        protected override void Update()
        {
            base.Update();

            if (_isCurrentlyCharging)
                UpdateValue(_chargeRate * Time.deltaTime);
        }

        public override void UpdateValue(float value)
        {
            currentValue += value;
            if (value < 0)
            {
                RestartRegenCountdown();
                RefreshColliderEnabledStates();
            }

            onUpdateDisplay?.Invoke();
        }

        private void UseCharge(float energyRequested)
        {
            var conversionRatio = energyRequested / transferRate;

            if (conversionRatio < currentValue)
            {
                device.UpdateValue(energyRequested);
                currentValue -= conversionRatio;
                onUpdateDisplay.Invoke();
            }
        }

        private void StartCharging(float value)
        {
            if (CanRegen)
            {
                _isCurrentlyCharging = true;
                _chargeRate = value;
            }
        }

        private void StopCharging() => _isCurrentlyCharging = false;
    }
}