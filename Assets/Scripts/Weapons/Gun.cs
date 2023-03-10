using System.Collections;
using Data.Scripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Damagers;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GunMod gunMod;
        [SerializeField] private GameObject muzzle;

        private bool _canFire;
        private bool _fireRequested;
        private Vector3 _baseMuzzlePosition;

        public GunStats gunStats;

        private void Awake()
        {
            _baseMuzzlePosition = muzzle.transform.position;
            SetGunMod(gunMod);
        }

        private void Update()
        {
            if (_fireRequested)
            {
                Fire();

                if (!gunStats.isAutomatic)
                    _fireRequested = false;
            }
        }

        public void FireRequest(InputAction.CallbackContext ctx) => _fireRequested = !ctx.canceled;

        private void Fire()
        {
            if (!_canFire) { }
            else
            {
                _canFire = false;
                StartCoroutine(GunFireCooldown());

                // check to see if the projectile will hit something
                Vector3 direction;
                UnityEngine.Physics.Raycast(transform.position, transform.forward, out var hit, gunStats.range * 5);

                // the projectile will either fly straight for its range or will fly towards the first target in range
                if (hit.point != Vector3.zero)
                    direction = hit.point - muzzle.transform.position;
                else
                    direction = transform.position + transform.forward * gunMod.range - muzzle.transform.position;

                direction.Normalize();

                // create the projectile
                GameObject projectileGo;
                Projectile projectile;

                if (gunStats.critChance >= Random.Range(0f, 100f))
                {
                    projectileGo = Instantiate(gunStats.projectile.critModel, muzzle.transform.position,
                        quaternion.identity);
                    projectile = projectileGo.GetComponent<Projectile>();
                    projectile.WillCriticallyHit = true;
                }
                else
                {
                    projectileGo = Instantiate(gunStats.projectile.baseModel, muzzle.transform.position,
                        quaternion.identity);
                    projectile = projectileGo.GetComponent<Projectile>();
                }

                projectileGo.transform.rotation = transform.rotation;

                // store data in the projectile
                projectile.MaxDistance = gunStats.range;
                projectile.ProjectileSpeed = gunStats.projectileSpeed;
                projectile.Direction = direction;
                projectile.CritDamageMultiplier = gunStats.critDamageMultiplier;

                var damage = Random.Range(gunStats.damageMin, gunStats.damageMax);
                projectile.Damage = projectile.WillCriticallyHit ? damage * gunStats.critDamageMultiplier : damage;
            }
        }

        public void Secondary(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                gunMod.onStarted?.Invoke();
            else if (ctx.performed)
                gunMod.onPerformed?.Invoke();
            else if (ctx.canceled)
                gunMod.onCanceled?.Invoke();
            else
                Debug.LogError($"Callback context could not be handled ({ctx})");
        }

        private void SetGunMod(GunMod mod)
        {
            gunMod = mod;
            gunStats = new GunStats(gunMod.damageMin, gunMod.damageMax, gunMod.critChance, gunMod.critDamageMultiplier, 
                gunMod.isAutomatic, gunMod.timeBetweenShots, gunMod.range, gunMod.projectileSpeed, gunMod.projectile, gunMod.model);
            
            var modGo = Instantiate(gunMod.model, _baseMuzzlePosition, Quaternion.identity, transform);
            var modController = modGo.GetComponent<GunModModelController>();
            muzzle.transform.position = modController.GetMuzzlePosition();
            
            StopAllCoroutines();
            _canFire = true;
        }

        private IEnumerator GunFireCooldown()
        {
            yield return new WaitForSeconds(gunStats.timeBetweenShots);
            _canFire = true;
        }

        public struct GunStats
        {
            public float damageMin;
            public float damageMax;
            public float critChance;
            public float critDamageMultiplier;
            public bool isAutomatic;
            public float timeBetweenShots;
            public float range;
            public float projectileSpeed;
            public ProjectileData projectile;
            public GameObject model;

            public GunStats(float damageMin, float damageMax, float critChance, float critDamageMultiplier, bool isAutomatic, 
                float timeBetweenShots, float range, float projectileSpeed, ProjectileData projectile, GameObject model)
            {
                this.damageMin = damageMin;
                this.damageMax = damageMax;
                this.critChance = critChance;
                this.critDamageMultiplier = critDamageMultiplier;
                this.isAutomatic = isAutomatic;
                this.timeBetweenShots = timeBetweenShots;
                this.range = range;
                this.projectileSpeed = projectileSpeed;
                this.projectile = projectile;
                this.model = model;
            }
        }
    }
}