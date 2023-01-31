using UnityEngine;
using UnityEngine.Events;

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
        public bool isAutomatic;
        public float timeBetweenShots;
        public float range;
        public float projectileSpeed;
        public ProjectileData projectile;

        [Header("Secondary Actions")] 
        public UnityEvent onStarted;
        public UnityEvent onPerformed;
        public UnityEvent onCanceled;
        
        [Header("Misc")] 
        public GameObject model;
    }
}