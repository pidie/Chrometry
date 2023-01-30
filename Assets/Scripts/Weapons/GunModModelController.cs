using UnityEngine;

namespace Weapons
{
    public class GunModModelController : MonoBehaviour
    {
        [SerializeField] private GameObject muzzlePosition;

        public Vector3 GetMuzzlePosition() => muzzlePosition.transform.position;
    }
}
