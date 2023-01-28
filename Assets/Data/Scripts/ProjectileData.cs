using UnityEngine;

namespace Data.Scripts
{
    [CreateAssetMenu(menuName = "Gun/Projectile", fileName = "New Projectile")]
    public class ProjectileData : ScriptableObject
    {
        public GameObject model;
    }
}