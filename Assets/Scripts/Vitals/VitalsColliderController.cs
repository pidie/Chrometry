using Interfaces;
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
            var damager = other.GetComponent<IDamager>();

            if (damager != null)
            {
                damager.SetIsInDamagerCollider(true);
                TimeSinceLastCollision = 0;
                HandleIDamagerCollision(damager);
            }
        }

        protected void OnCollisionEnter(Collision other)
        {
            var damager = other.gameObject.GetComponent<IDamager>();

            if (damager != null)
            {
                damager.SetIsInDamagerCollider(true);
                TimeSinceLastCollision = 0;
                HandleIDamagerCollision(damager);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            var damager = other.GetComponent<IDamager>();

            if (damager != null)
            {
                damager.SetIsInDamagerCollider(false);
            }
        }

        protected void OnCollisionExit(Collision other)
        {
            var damager = other.gameObject.GetComponent<IDamager>();

            if (damager != null)
            {
                damager.SetIsInDamagerCollider(false);
            }
        }

        protected void ToggleCollider(bool value)
        {
            _collider.enabled = value;
            vitalsController.SetColliderEnabled(this);
        }


        protected void HandleIDamagerCollision(IDamager damager)
        {
            var damage = damager.Damage;
            
            if (damager.WillCriticallyHit)
                damage *= damager.CritDamageMultiplier;
            
            vitalsController.UpdateValue(damage * -1f * damageMultiplier);
            damager.CollideWithObject();
        }

        public bool GetColliderEnabledState() => _collider.enabled;
    }
}