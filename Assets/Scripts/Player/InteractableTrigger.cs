using UnityEngine;

namespace Player
{
    public class InteractableTrigger : MonoBehaviour
    {
        public PlayerController PlayerController { get; set; }

        private void Awake() => PlayerController = GetComponentInParent<PlayerController>();
    }
}
