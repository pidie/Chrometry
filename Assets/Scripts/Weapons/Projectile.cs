using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        public float MaxDistance { get; set; }
        public float ProjectileSpeed { get; set; }
        public Vector3 Direction { get; set; }
        public float Damage { get; set; }
        
        public bool willCriticallyHit;

        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        private void FixedUpdate()
        {
            transform.position += Direction * ProjectileSpeed;
            if (Vector3.Distance(_initialPosition, transform.position) > MaxDistance)
                Destroy(gameObject);
        }

        // empty overload should be used for when the projectile hits something unimportant but still needs to register a collision
        public void CollideWithObject() => Destroy(gameObject);
        
        // create overloads for different types of collisions (ie enemies, player, etc)
        // components should be able to just call projectile.Collide(this) and this script will handle the rest
        public void CollideWithObject(GameObject obj) { }
    }
}