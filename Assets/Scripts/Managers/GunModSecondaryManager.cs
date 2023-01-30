using System;
using Player;
using UnityEngine;
using Weapons;

namespace Managers
{
    public class GunModSecondaryManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject cameraPosition;
        [SerializeField] private Gun gun;

        [Header("Pistol")] 
        [SerializeField] private float pistolCritChanceIncrease;
        [SerializeField] private float pistolTimeBetweenShotsIncreaseMultiplier;
        
        private static PlayerController _playerController;
        private static GameObject _cameraPosition;
        private static Gun _gun;

        private static float _pistolCritChanceIncrease;
        private static float _pistolTimeBetweenShotsIncreaseMultiplier;

        private static float _cachedPistolEnergyCostToFire;
        private static float _cachedPistolCritChance;
        private static float _cachedPistolTimeBetweenShots;


        private void Awake()
        {
            _playerController = playerController;
            _cameraPosition = cameraPosition;
            _gun = gun;
            
            _pistolCritChanceIncrease = pistolCritChanceIncrease;
            _pistolTimeBetweenShotsIncreaseMultiplier = pistolTimeBetweenShotsIncreaseMultiplier;
        }

        public static void PistolOnStarted() { }
        
        public static void PistolOnPerformed()
        {
            var gunStats = _gun.gunStats;

            _cachedPistolCritChance = gunStats.critChance;
            _cachedPistolTimeBetweenShots = gunStats.timeBetweenShots;

            _gun.gunStats.critChance += _pistolCritChanceIncrease;
            _gun.gunStats.timeBetweenShots *= _pistolTimeBetweenShotsIncreaseMultiplier;
        }

        public static void PistolOnCanceled()
        {
            _gun.gunStats.critChance = _cachedPistolCritChance;
            _gun.gunStats.timeBetweenShots = _cachedPistolTimeBetweenShots;
        }
    }
}