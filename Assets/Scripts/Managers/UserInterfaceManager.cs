using System;
using UnityEngine;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour
    {
        private GameObject _uiContainer;
        public static GameObject LastCreatedCanvas { get; private set; }
    
        public static Action<GameObject> onCreateCanvas;

        private void Awake() => _uiContainer = GameObject.Find("*** UI");

        private void OnEnable() => onCreateCanvas += CreateCanvas;

        private void OnDisable() => onCreateCanvas -= CreateCanvas;

        public void CreateCanvas(GameObject canvas) => LastCreatedCanvas = Instantiate(canvas, _uiContainer.transform);
    }
}