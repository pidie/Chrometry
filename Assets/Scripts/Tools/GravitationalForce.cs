using Physics;
using UnityEngine;

namespace Tools
{
    /// <summary>
    /// Adds artificial gravity to gameObjects. Requires a <see cref="GroundCheck"/> reference.
    /// </summary>
    public class GravitationalForce : MonoBehaviour
    {
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private float gravityMultiplier = 1f;
    
        private Vector3 _velocity;
        private CharacterController _characterController;
    
        private const float Gravity = -9.81f;

        public GroundCheck GetGroundCheck() => groundCheck;

        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void FixedUpdate()
        {
            if (groundCheck.CheckIsGrounded() && _velocity.y <= 0)
            {
                _velocity.y = 0f;
                return;
            }

            _velocity.y += Gravity * Time.deltaTime * gravityMultiplier;

            if (_characterController)
                _characterController.Move(_velocity * Time.deltaTime);
            else
                transform.position += _velocity * Time.deltaTime;
        }

        public void Jump(float jumpHeight)
        {
            if (groundCheck.CheckIsGrounded())
                _velocity.y = Mathf.Sqrt(jumpHeight * -2 * Gravity);
        }

        // used for explosion effects
        public void Throw(Vector3 direction, float force)
        {
            var directionX = Mathf.Sqrt(direction.x * -2 * Gravity);
            var directionY = Mathf.Sqrt(direction.y * -2 * Gravity);
            var directionZ = Mathf.Sqrt(direction.z * -2 * Gravity);

            _velocity = new Vector3(directionX, directionY, directionZ).normalized * force;
        }
    }
}