using UnityEngine;

namespace Vitals
{
    public class HealthDisplayUI : VitalsDisplayUI
    {
        protected override void UpdateDisplay()
        {
            base.UpdateDisplay();

            if (iconPlayer != null)
            {
                var maxHealth = vitalsController.MaxValue;
                var twentyPercent = maxHealth * 0.2f;
                var eightyPercent = maxHealth * 0.8f;
                var currentHealth = vitalsController.CurrentValue;
                
                if (currentHealth >= twentyPercent)
                {
                    var percentage = (currentHealth - twentyPercent) / eightyPercent;
                    iconPlayer.fillAmount = 1f;
                    iconPlayer.color = Color.Lerp(minHealthColor, maxHealthColor, percentage);
                }
                else
                {
                    iconPlayer.fillAmount = currentHealth / twentyPercent;
                    iconPlayer.color = minHealthColor;
                }
            }
        }
    }
}