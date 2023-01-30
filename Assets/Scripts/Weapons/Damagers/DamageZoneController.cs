using System.Collections;
using Interfaces;
using UnityEngine;

namespace Weapons.Damagers
{
    public class DamageZoneController : MonoBehaviour, IDamager
    {
        [SerializeField] private float damagePerTick;
        [SerializeField] private float tickDuration = 0.25f;

        private bool _collider;
        private bool _canTakeDamage;
        private bool _isInDamagerCollider;
        
        private int _playerMask;
        private int _damageZoneMask;
        
        public float Damage { get; set; }
        public bool WillCriticallyHit { get; set; }
        public float CritDamageMultiplier { get; set; }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            Damage = damagePerTick;
            _playerMask = LayerMask.NameToLayer("Player");
            _damageZoneMask = LayerMask.NameToLayer("DamageZone");
            _canTakeDamage = true;
        }

        private void Update()
        {
            if (_canTakeDamage && _isInDamagerCollider)
            {
                _canTakeDamage = false;
                StartCoroutine(WaitTick());
            }
        }

        private IEnumerator WaitTick()
        { 
            UnityEngine.Physics.IgnoreLayerCollision(_playerMask, _damageZoneMask, true);
            yield return new WaitForSeconds(tickDuration);
            UnityEngine.Physics.IgnoreLayerCollision(_playerMask, _damageZoneMask, false);
            _canTakeDamage = true;
        }
        
        public void CollideWithObject()
        {
            
        }

        public void SetIsInDamagerCollider(bool value) => _isInDamagerCollider = value;
    }
}
