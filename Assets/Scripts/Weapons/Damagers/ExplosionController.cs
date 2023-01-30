using System.Collections;
using Interfaces;
using UnityEngine;

namespace Weapons.Damagers
{
    public class ExplosionController : MonoBehaviour, IDamager
    {
        [SerializeField] private GameObject rootObject;
        [SerializeField] private GameObject explosionSphere;
        [SerializeField] private float expansionSpeed;
        [SerializeField] private float range;
        [SerializeField] private float damage;
        [SerializeField] private new Light light;
        [SerializeField] private float lightIntensity;

        private Material _explosionMaterial;
        private Color _explosionColor;
        private Vector3 _startScale;
        private Vector3 _newScale;
        
        public float Damage { get; set; }
        public bool WillCriticallyHit { get; set; }
        public float CritDamageMultiplier { get; set; }

        private void Awake()
        {
            Damage = damage;
            _explosionMaterial = explosionSphere.GetComponent<MeshRenderer>().material;
            _explosionColor = _explosionMaterial.color;
            light.color = _explosionColor;
            light.intensity = lightIntensity;
            light.range = range;
            _startScale = explosionSphere.transform.localScale;
            StartCoroutine(GrowExplosion());
        }

        private void Update()
        {
            print(_startScale.x);
            explosionSphere.transform.localScale = _newScale;
            
            var newSize = _newScale.x + expansionSpeed * Time.deltaTime;
            _explosionMaterial.color = Color.Lerp(_explosionColor, Color.clear, _newScale.x / range);
            _newScale = new Vector3(newSize, newSize, newSize);

            if (_newScale.x >= range)
                Destroy(rootObject);
        }

        private IEnumerator GrowExplosion()
        {
            // var newSize = Mathf.Lerp(_startScale.x, range, (expansionSpeed + 1) * Time.deltaTime);
            // _explosionMaterial.color = Color.Lerp(_explosionColor, Color.clear, _newScale.x / range);
            // _newScale = new Vector3(newSize, newSize, newSize);

            yield return null;
        }

        public void CollideWithObject() { }
        
        public void SetIsInDamagerCollider(bool value) { }
    }
}