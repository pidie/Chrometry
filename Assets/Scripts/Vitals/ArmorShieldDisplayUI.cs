namespace Vitals
{
	public class ArmorShieldDisplayUI : VitalsDisplayUI
	{
		protected override void UpdateDisplay()
		{
			base.UpdateDisplay();
			
			var currentValue = vitalsController.CurrentValue;
			var maxValue = vitalsController.MaxValue;
			
			iconPlayer.fillAmount = currentValue / maxValue;
		}
	}
}