using System;
using UnityEngine;

namespace Tools
{
    public class RotatingObject : MonoBehaviour
    {
        [SerializeField] private RotationInfo xRotInfo;
        [SerializeField] private RotationInfo yRotInfo;
        [SerializeField] private RotationInfo zRotInfo;

        private float _xDifference;
        private float _yDifference;
        private float _zDifference;

        private float _xAverage;
        private float _yAverage;
        private float _zAverage;

        private void Update()
        {
            // ensure that min values are not bigger than max values
            xRotInfo.ValidateRot();
            yRotInfo.ValidateRot();
            zRotInfo.ValidateRot();
        
            _xAverage = (xRotInfo.GetRotMin() + xRotInfo.GetRotMax()) / 2;
            _yAverage = (yRotInfo.GetRotMin() + yRotInfo.GetRotMax()) / 2;
            _zAverage = (zRotInfo.GetRotMin() + zRotInfo.GetRotMax()) / 2;

            _xDifference = xRotInfo.GetRotMax() - _xAverage;
            _yDifference = yRotInfo.GetRotMax() - _yAverage;
            _zDifference = zRotInfo.GetRotMax() - _zAverage;
        
            var xRot = Mathf.Sin(Time.time * xRotInfo.GetRotIntensity()) * _xDifference + _xAverage;
            var yRot = Mathf.Sin(Time.time * yRotInfo.GetRotIntensity()) * _yDifference + _yAverage;
            var zRot = Mathf.Sin(Time.time * zRotInfo.GetRotIntensity()) * _zDifference + _zAverage;
        
            transform.Rotate(xRot, yRot, zRot);
        }

        [Serializable]
        private class RotationInfo
        {
            [Tooltip("The minimum speed")]
            [SerializeField] private float rotMin;
            [Tooltip("The maximum speed")]
            [SerializeField] private float rotMax;
            [Tooltip("The rate of oscillation")]
            [SerializeField] private float rotIntensity;

            public float GetRotMin() => rotMin;
            public float GetRotMax() => rotMax;
            public float GetRotIntensity() => rotIntensity;

            public void ValidateRot()
            {
                if (rotMin > rotMax || rotMax < rotMin)
                    rotMin = rotMax;
            }
        }
    }
}
