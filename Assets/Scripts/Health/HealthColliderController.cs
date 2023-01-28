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
                HandleProjectileCollision(projectile);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile)
                HandleProjectileCollision(projectile);
        }

        private void HandleProjectileCollision(Projectile projectile)
        {
            // call the Health to decrease health based on the damage
            healthController.UpdateHealth(projectile.Damage * -1f * damageModifier);
        
            // call the Projectile to handle its collision
            projectile.CollideWithObject();
        }
    }
}