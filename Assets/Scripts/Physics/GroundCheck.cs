using UnityEngine;

namespace Physics
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField, Tooltip("Defaults to the transform")] private Transform checker;
        [SerializeField] private float sphereRadius = 0.4f;
        [SerializeField, Tooltip("Defaults to the Ground layer")] private LayerMask layerMask;

        private void Awake()
        {
            if (checker == null) checker = transform;

            if (layerMask == default)
                layerMask = 1 << LayerMask.NameToLayer("Ground");
        }

        /// <summary>
        /// Returns true if the game object is grounded.
        /// </summary>
        /// <returns></returns>
        public bool CheckIsGrounded() => UnityEngine.Physics.CheckSphere(checker.position, sphereRadius, layerMask);
    }
}