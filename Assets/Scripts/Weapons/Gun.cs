using Data.Scripts;
using Unity.Mathematics;
using Unity.VisualScripting;
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

        private void Awake() => SetGunMod(gunMod);

        public void Fire()
        {
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
            var projectileGo = _critChance >= Random.Range(0f, 100f) 
                ? Instantiate(gunMod.projectile.critModel, muzzle.transform.position, quaternion.identity) 
                : Instantiate(gunMod.projectile.baseModel, muzzle.transform.position, quaternion.identity);
            var projectile = projectileGo.GetComponent<Projectile>();
            projectileGo.transform.rotation = transform.rotation;
            
            // store data in the projectile
            projectile.MaxDistance = gunMod.range;
            projectile.ProjectileSpeed = gunMod.projectileSpeed;
            projectile.Direction = direction;

            // if (_critChance >= Random.Range(0f, 100f))
            // {
            //     projectile.WillCriticallyHit = true;
            //     var light = projectile.AddComponent<Light>();
            //     light.type = LightType.Point;
            //     light.intensity = 10 * _critDamageMultiplier;
            //     light.color = Color.red;
            //     light.transform.position = projectile.transform.position;
            //     light.transform.parent = projectile.transform;
            // }
            projectile.CritDamageMultiplier = _critDamageMultiplier;

            var damage = Random.Range(gunMod.damageMin, gunMod.damageMax);
            projectile.Damage = projectile.WillCriticallyHit ? damage * _critDamageMultiplier : damage;
        }

        private void SetGunMod(GunMod mod)
        {
            gunMod = mod;
            _critChance = mod.critChance;
            _critDamageMultiplier = mod.critDamageMultiplier;
        }
    }
}