using UnityEngine;

namespace Tools
{
    public class DestroyProjectiles : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var projectile = other.gameObject.GetComponent<Weapons.Damagers.Projectile>();
        
            if (projectile)
                projectile.CollideWithObject();
        }
    }
}