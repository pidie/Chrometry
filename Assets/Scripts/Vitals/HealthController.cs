using System;

namespace Vitals
{
    public class HealthController : VitalsController
    {
        public Action onDeath;

        public override void UpdateValue(float value)
        {
            currentValue += value;
            if (value < 0)
            {
                canRegen = false;
                StopAllCoroutines();
                StartCoroutine(RegenDelay());

                if (currentValue < 0)
                    onDeath.Invoke();
            }

            onUpdateDisplay?.Invoke();
        }
    }
}