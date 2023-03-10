using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vitals
{
	[RequireComponent(typeof(Image))]
	public abstract class VitalsDisplayUI : MonoBehaviour
	{
		[SerializeField] protected VitalsController vitalsController;
		[SerializeField] protected Color maxVitalsColor;
		[SerializeField] protected Color minVitalsColor;

		protected TMP_Text text;
		protected Image iconPlayer;
		protected Image icon;

		protected void Awake()
		{
			text = GetComponent<TMP_Text>();
			if (text == null)
				text = GetComponentInChildren<TMP_Text>();

			if (vitalsController.GetComponent<Player.PlayerController>())
				iconPlayer = GetComponent<Image>();
			else
				icon = GetComponent<Image>();

			UpdateDisplay();
		}

		protected void OnEnable() => vitalsController.onUpdateDisplay += UpdateDisplay;

		protected void OnDisable() => vitalsController.onUpdateDisplay -= UpdateDisplay;

		protected virtual void UpdateDisplay()
		{
			if (text != null)
				text.text = vitalsController.CurrentValue.ToString();
			
			// todo : insert generic health bar method here - for enemies and destructibles with visible health
		}
	}
}