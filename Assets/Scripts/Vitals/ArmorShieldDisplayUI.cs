namespace Vitals
{
	public class ArmorShieldDisplayUI : VitalsDisplayUI
	{
		protected override void UpdateDisplay()
		{
			base.UpdateDisplay();
			
			var currentProtection = vitalsController.CurrentValue;
			var maxProtection = vitalsController.MaxValue;
			
			iconPlayer.fillAmount = currentProtection / maxProtection;
		}
	}
}