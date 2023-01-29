using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vitals
{
    public abstract class VitalsController : MonoBehaviour
    {
        [SerializeField] protected float maxValue;
        [SerializeField] protected float currentValueOverride;
        [SerializeField] protected float vitalRegen;
        [SerializeField] protected float vitalRegenDelay;

        protected float currentValue;
        protected bool canRegen;
        protected Dictionary<VitalsColliderController, bool> colliderControllers = new ();

        public Action onUpdateDisplay;
        public Action<bool> onToggleCollider;

        public float CurrentValue => currentValue;
        public float MaxValue => maxValue;

        public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)
        {
            maxValue = value;

            if (maxOutCurrentValue)
                currentValue = maxValue;
            else if (increaseCurrentValue)
                UpdateValue(value);
        }

        protected virtual void Awake()
        {
            currentValue = currentValueOverride < maxValue ? maxValue : currentValueOverride;
            onUpdateDisplay?.Invoke();
        }

        protected void Update()
        {
            if (currentValue > maxValue) currentValue = maxValue;
            if (currentValue < maxValue && canRegen)
            {
                currentValue += vitalRegen * Time.deltaTime;
                onUpdateDisplay?.Invoke();
            }
        }

        public virtual void UpdateValue(float value)
        {            
            currentValue += value;
            if (value < 0)
            {
                canRegen = false;
                StopAllCoroutines();
                StartCoroutine(RegenDelay());

                RefreshColliderEnabledStates();
            }

            onUpdateDisplay?.Invoke();
        }

        public void UpdateMaxValue(float value) => maxValue += value;

        protected IEnumerator RegenDelay()
        {
            if (!QueryColliderIsEnabled() && QueryLastCollision() < vitalRegenDelay) 
                yield return null;
            else
            {
                yield return new WaitForSeconds(vitalRegenDelay);
                canRegen = true;
            }
        }

        public void RegisterColliderToController(VitalsColliderController colliderController)
        {
            if (colliderControllers.ContainsKey(colliderController)) return;
            
            colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
        }

        public void SetColliderEnabled(VitalsColliderController colliderController)
        {
            foreach (var key in colliderControllers.Keys)
            {
                if (key == colliderController)
                {
                    colliderControllers[key] = colliderController.GetColliderEnabledState();
                    return;
                }
            }
            
            colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
            print($"added new VitalsColliderController {colliderController} with a value of {colliderControllers[colliderController]}");
        }

        /// <summary>
        /// Returns true if any registered colliders are enabled.
        /// </summary>
        /// <returns></returns>
        public bool QueryColliderIsEnabled() => colliderControllers.Values.Any(value => value);

        /// <summary>
        /// Returns the amount of time since the last collision registered to this controller.
        /// </summary>
        /// <returns></returns>
        public float QueryLastCollision()
        {
            var shortestTime = Mathf.Infinity;

            foreach (var key in colliderControllers.Keys)
            {
                if (key.TimeSinceLastCollision < shortestTime)
                    shortestTime = key.TimeSinceLastCollision;
            }

            return shortestTime;
        }
        
        protected void RestartRegenCountdown()
        {
            canRegen = false;
            StopAllCoroutines();
            StartCoroutine(RegenDelay());
        }

        protected void RefreshColliderEnabledStates()
        {
            var registeredColliders = colliderControllers.Keys;
            colliderControllers.Clear();

            foreach (var colliderController in registeredColliders)
                colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
        }
    }
}