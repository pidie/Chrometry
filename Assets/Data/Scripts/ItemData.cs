using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Data.Scripts
{
    [CreateAssetMenu(menuName = "Item", fileName = "New Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public UnityEvent functions;
        [Tooltip("Used for the Score Multiplier item")]
        public float scoreMultiplier;
        [TextArea(5,10)]
        public string description;
    }
}