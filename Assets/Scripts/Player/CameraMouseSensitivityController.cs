using Cinemachine;
using UnityEngine;

namespace Player
{
    // todo : turn this class into a controller for all look input
    public class CameraMouseSensitivityController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cam;

        private CinemachinePOV _pov;

        /// <summary>
        /// Updates the max speed of movement of each axis by a percentage, assuming a base max speed of 300.
        /// </summary>
        /// <param name="horizontalSpeedModifier"></param>
        /// <param name="verticalSpeedModifier"></param>
        public void UpdateMouseSensitivity(float horizontalSpeedModifier = 1f, float verticalSpeedModifier = default)
        {
            if (_pov == default)
                _pov = cam.GetCinemachineComponent<CinemachinePOV>();
            
            _pov.m_HorizontalAxis.m_MaxSpeed *= horizontalSpeedModifier;
            _pov.m_VerticalAxis.m_MaxSpeed *= verticalSpeedModifier == default ? horizontalSpeedModifier : verticalSpeedModifier;
        }
    }
}