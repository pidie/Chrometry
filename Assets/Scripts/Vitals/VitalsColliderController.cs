using UnityEngine;

namespace Vitals
{
    [RequireComponent(typeof(Collider))]
    public class VitalsColliderController : MonoBehaviour
    {
        [SerializeField] protected float damageMultiplier = 1f;
        [SerializeField] protected VitalsController vitalsController;

        protected Collider _collider;

        public float TimeSinceLastCollision { get; private set; }

        protected void Awake()
        {
            _collider = GetComponent<Collider>();
            vitalsController.RegisterColliderToController(this);
        }

        protected void Update() => TimeSinceLastCollision += Time.deltaTime;

        protected void OnEnable() => vitalsController.onToggleCollider += ToggleCollider;

        protected void OnDisable() => vitalsController.onToggleCollider -= ToggleCollider;

        protected void OnTriggerEnter(Collider other)
        {
            var projectile = other.gameObject.GetComponent<Weapons.Projectile>();

            if (projectile)
            {
                TimeSinceLastCollision = 0;
                HandleIDamagerCollision(projectile);
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            var projectile = collision.gameObject.GetComponent<Weapons.Projectile>();

            if (projectile)
            {
                TimeSinceLastCollision = 0;
                HandleIDamagerCollision(projectile);
            }
        }

        protected void ToggleCollider(bool value)
        {
            _collider.enabled = value;
            vitalsController.SetColliderEnabled(this);
        }


        protected void HandleIDamagerCollision(Interfaces.IDamager damager)
        {
            vitalsController.UpdateValue(damager.Damage * -1f * damageMultiplier);
            damager.CollideWithObject();
        }

        public bool GetColliderEnabledState() => _collider.enabled;
    }
}