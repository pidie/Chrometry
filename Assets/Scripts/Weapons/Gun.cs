using System.Collections;
using Data.Scripts;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GunMod gunMod;
        [SerializeField] private GameObject muzzle;

        private float _critChance;
        private float _critDamageMultiplier;
        private bool _canFire;
        private Vector3 _baseMuzzlePosition;

        private void Awake()
        {
            _baseMuzzlePosition = muzzle.transform.position;
            SetGunMod(gunMod);
        }

        public void Fire()
        {
            if (!_canFire) return;
            _canFire = false;
            StartCoroutine(GunFireCooldown());
            
            // check to see if the projectile will hit something
            Vector3 direction;
            UnityEngine.Physics.Raycast(transform.position, transform.forward, out var hit, gunMod.range * 5);
            
            // the projectile will either fly straight for its range or will fly towards the first target in range
            if (hit.point != Vector3.zero)
                direction = hit.point - muzzle.transform.position;
            else
                direction = transform.position + transform.forward * gunMod.range - muzzle.transform.position;

            direction.Normalize();

            // create the projectile
            GameObject projectileGo;
            Projectile projectile;
            
            if (_critChance >= Random.Range(0f, 100f))
            {
                projectileGo = Instantiate(gunMod.projectile.critModel, muzzle.transform.position, quaternion.identity);
                projectile = projectileGo.GetComponent<Projectile>();
                projectile.WillCriticallyHit = true;
            }
            else
            {
                projectileGo = Instantiate(gunMod.projectile.baseModel, muzzle.transform.position, quaternion.identity);
                projectile = projectileGo.GetComponent<Projectile>();
            }
            
            projectileGo.transform.rotation = transform.rotation;
            
            // store data in the projectile
            projectile.MaxDistance = gunMod.range;
            projectile.ProjectileSpeed = gunMod.projectileSpeed;
            projectile.Direction = direction;
            projectile.CritDamageMultiplier = _critDamageMultiplier;

            var damage = Random.Range(gunMod.damageMin, gunMod.damageMax);
            projectile.Damage = projectile.WillCriticallyHit ? damage * _critDamageMultiplier : damage;
        }

        private void SetGunMod(GunMod mod)
        {
            gunMod = mod;
            _critChance = mod.critChance;
            _critDamageMultiplier = mod.critDamageMultiplier;
            
            var modGo = Instantiate(gunMod.model, _baseMuzzlePosition, Quaternion.identity, transform);
            var modController = modGo.GetComponent<GunModModelController>();
            muzzle.transform.position = modController.GetMuzzlePosition();
            
            StopAllCoroutines();
            _canFire = true;
        }

        private IEnumerator GunFireCooldown()
        {
            yield return new WaitForSeconds(gunMod.timeBetweenShots);
            _canFire = true;
        }
    }
}