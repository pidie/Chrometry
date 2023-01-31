using UnityEngine;

namespace Tools
{
    public class AverageCenter : MonoBehaviour
    {
        [SerializeField] private float intensity = 0.25f;
        [SerializeField] private GameObject[] objects;

        private Vector3 _center;

        private void Awake()
        {
            _center = transform.position;
        }

        private void Update()
        {
            if (objects.Length == 0) return;
            
            var sumOfVectors = Vector3.zero;

            foreach (var go in objects)
            {
                var pos = go.transform.position;
                sumOfVectors += pos;
            }

            var averagePosition = sumOfVectors / objects.Length;

            transform.position = Vector3.MoveTowards(_center, averagePosition, intensity);
        }
    }
}
