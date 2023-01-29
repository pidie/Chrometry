using Data.Scripts;
using Interfaces;
using UnityEngine;
using Vitals;

namespace ObjectControllers
{
    public class ReactorController : MonoBehaviour, IDestructable
    {
        [SerializeField] private HealthController healthController;
    
        public DestructableData data;
        public float StructureHealth { get; set; }

        private void Awake() => healthController.SetMaxValue(data.structureHealth);

        private void OnEnable() => healthController.onDeath += OnDeath;

        private void OnDisable() => healthController.onDeath -= OnDeath;

        private void OnDeath() => Destroy(gameObject);
    }
}