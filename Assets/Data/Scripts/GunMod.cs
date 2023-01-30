using UnityEngine;

namespace Data.Scripts
{
    [CreateAssetMenu(menuName = "Gun/Gun Mod", fileName = "New Gun Mod")]
    public class GunMod : ScriptableObject
    {
        [Header("Damage")]
        public float damageMin;
        public float damageMax;
        public float critChance;
        public float critDamageMultiplier = 1f;
        
        [Header("Specs")]
        public float timeBetweenShots;
        public float range;
        public float projectileSpeed;
        public ProjectileData projectile;

        [Header("Misc")] 
        public GameObject model;
        // public ParticleSystem criticalHitParticleEffect;

        /*
         * things to track:
         * - which InputAction events are tracked during the secondary action for this gun mod?
         * - what does the secondary function of this gun mod do?
         */
    }
}