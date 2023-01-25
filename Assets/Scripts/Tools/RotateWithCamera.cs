using UnityEngine;

namespace Tools
{
    public class RotateWithCamera : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;

        private void Update() => transform.rotation = targetCamera.transform.rotation;
    }
}
