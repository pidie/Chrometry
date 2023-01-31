using System;
using UnityEngine;

namespace Vitals
{
    public class EnergyController : VitalsController
    {
        [SerializeField] private VitalsController device;
        [SerializeField] private float transferRate;

        public float TransferRate => transferRate;

        public Action<float> onUseCharge;

        protected override void Awake()
        {
            onUseCharge += UseCharge;
            base.Awake();
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
    }
}