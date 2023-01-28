namespace Interfaces
{
	public interface IDamager
	{
		public float Damage { get; set; }

		public void CollideWithObject();
	}
}