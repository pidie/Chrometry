using UnityEngine;
using Weapons;
using Weapons.Damagers;

namespace Tools
{
    public class DestroyProjectiles : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
        
            if (projectile)
                projectile.CollideWithObject();
        }
    }
}