using UnityEngine;

namespace Tools
{
    public class BobbingObject : MonoBehaviour
    {
        [SerializeField] private float minHeight;
        [SerializeField] private float maxHeight;
        [SerializeField] private float moveSpeed;
        [SerializeField] private GravitationalForce gravitationalForce;

        private float _centerPositionY;
        private float _heightDifference;

        private void Awake()
        {
            _centerPositionY = (minHeight + maxHeight) / 2;
            _heightDifference = maxHeight - _centerPositionY;
        }

        private void FixedUpdate()
        {
            // if the object is not currently falling, it can bob in place
            if (gravitationalForce)
            {
                if (gravitationalForce.GetGroundCheck().CheckIsGrounded())
                    Bob();
            }
            else
                Bob();
        }

        private void Bob()
        {
            var moveDiff = Mathf.Sin(Time.time * moveSpeed) * _heightDifference + _centerPositionY;
            transform.localPosition = new Vector3(0, moveDiff, 0);
        }
    }
}