namespace Interfaces
{
	public interface IDamager
	{
		public float Damage { get; set; }
		public bool WillCriticallyHit { get; set; }
		public float CritDamageMultiplier { get; set; }

		public void CollideWithObject();
		public void SetIsInDamagerCollider(bool value);
	}
}