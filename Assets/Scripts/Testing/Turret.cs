using System.Collections;
using Data.Scripts;
using Unity.Mathematics;
using UnityEngine;
using Weapons;

namespace Testing
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private GunMod gunMod;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float damage;

        [SerializeField] private bool isTrue = true;

        private void Awake() => StartCoroutine(FireTurret());

        private void Fire()
        {
            var go = Instantiate(gunMod.projectile.model, muzzle.position, quaternion.identity);
            var projectile = go.AddComponent<Projectile>();
            projectile.Damage = damage;
            projectile.Direction = transform.forward;
            projectile.MaxDistance = 100f;
            projectile.ProjectileSpeed = 0.8f;
        }
        
        private IEnumerator FireTurret()
        {
            while (isTrue)
            {
                Fire();
                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }
}