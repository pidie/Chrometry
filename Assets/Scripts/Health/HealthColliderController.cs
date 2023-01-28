using Interfaces;
using UnityEngine;
using Weapons;

namespace Health
{
    [RequireComponent(typeof(Collider))]
    public class HealthColliderController : MonoBehaviour
    {
        [SerializeField] private float damageModifier = 1f;
        [SerializeField] private HealthController healthController;

        private void OnTriggerEnter(Collider other)
        {
            var projectile = other.gameObject.GetComponent<Projectile>();

            if (projectile)
                HandleIDamagerCollision(projectile);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            
            if (projectile)
                HandleIDamagerCollision(projectile);
        }
        
        /*
         * currently, only projectiles inherit from IDamager. eventually, there could be an explosion type or something else that would
         * deal damage.
         */
        private void HandleIDamagerCollision(IDamager damager)
        {
            // call the Health to decrease health based on the damage
            healthController.UpdateHealth(damager.Damage * -1f * damageModifier);
        
            // call the Projectile to handle its collision
            damager.CollideWithObject();
        }
    }
}