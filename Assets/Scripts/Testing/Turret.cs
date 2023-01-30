using System.Collections;
using Data.Scripts;
using UnityEngine;
using Weapons;

namespace Testing
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private GunMod gunMod;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float damage;

        private void Awake() => StartCoroutine(FireTurret());

        private void Fire()
        {
            var go = Instantiate(gunMod.projectile.baseModel, muzzle.position, Quaternion.identity);
            var projectile = go.GetComponent<Projectile>();
            projectile.Damage = damage;
            projectile.Direction = transform.forward;
            projectile.MaxDistance = 100f;
            projectile.ProjectileSpeed = 0.8f;
        }
        
        // perpetually fires a projectile every second
        private IEnumerator FireTurret()
        {
            while (true)
            {
                Fire();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}