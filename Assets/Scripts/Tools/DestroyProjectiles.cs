using UnityEngine;
using Weapons;

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