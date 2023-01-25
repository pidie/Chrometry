using Data.Scripts;
using Interfaces;
using UnityEngine;

namespace ObjectControllers
{
    public class ReactorController : MonoBehaviour, IDestructable
    {
        [SerializeField] private Health.HealthController healthController;
    
        public DestructableData data;
        public float StructureHealth { get; set; }

        private void Awake() => healthController.SetMaxHealth(data.structureHealth);

        private void OnEnable() => healthController.onDeath += OnDeath;

        private void OnDisable() => healthController.onDeath -= OnDeath;

        private void OnDeath() => Destroy(gameObject);
    }
}