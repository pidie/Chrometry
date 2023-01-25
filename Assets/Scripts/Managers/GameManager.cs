using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}