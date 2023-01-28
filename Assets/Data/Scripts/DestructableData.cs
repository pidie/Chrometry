using UnityEngine;

namespace Data.Scripts
{
    [CreateAssetMenu(menuName = "Objects/Destructable", fileName = "New Destructable")]
    public class DestructableData : ScriptableObject
    {
        public string structureName;
        public int structureHealth;
    }
}
