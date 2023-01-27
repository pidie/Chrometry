---
id: 5v1da
title: HealthController
file_version: 1.1.1
app_version: 1.0.20
---

The `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/> is the brain of the health system. There should only be one `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/> for any entity with a trackable health.<br/>

## Fields

`maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/> - stores the max health the entity has.

`healthRegen`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:22:23:23:`        [SerializeField, Tooltip(&quot;Health regenerated per second&quot;)] private float healthRegen = 1.5f;`"/> - how much health per second should be regenerated.

`healthRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:23:31:31:`        [SerializeField, Tooltip(&quot;Delay after taking damage before health will regenerate&quot;)] private float healthRegenDelay = 2.5f;`"/> - how many seconds after taking damage until health can be regenerated.

`_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> - the current health of the entity.

`_canRegen`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:26:5:5:`        private bool _canRegen;`"/> - tracks whether or not to regenerate health.

`onDeath`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:29:5:5:`        public Action onDeath;`"/> - invoked when the entity's health drops below 0.

`onUpdateHealthDisplay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:30:5:5:`        public Action onUpdateHealthDisplay;`"/> - invoked when the `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> has changed and the UI needs to update.<br/>

## Functions

`GetHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:32:5:5:`        public float GetHealth() =&gt; _currentHealth;`"/> - returns the value of `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/>

`GetMaxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:34:5:5:`        public float GetMaxHealth() =&gt; maxHealth;`"/> - returns the value of `maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/>

`SetMaxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:5:5:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/> - takes a float (`value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:9:9:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/>) and a bool (`updateCurrentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:14:14:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/>).

*   `maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/> will be set to `value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:9:9:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/>
    
*   If `updateCurrentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:14:14:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/> is true, `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> will also increase by `value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:9:9:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/>
    
*   `value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:9:9:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/>is set to `true`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:36:18:18:`        public void SetMaxHealth(float value, bool updateCurrentHealth = true)`"/> by default.
    

`Awake`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:44:5:5:`        private void Awake()`"/> - sets `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> to equal `maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/> and invokes `onUpdateHealthDisplay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:30:5:5:`        public Action onUpdateHealthDisplay;`"/>.

`Update`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:50:5:5:`        private void Update()`"/> - continually check the status of `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/>

*   if `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> is greater than `maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/>, sets `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> equal to it instead
    
*   if `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> is less than `maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/> and `_canRegen`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:26:5:5:`        private bool _canRegen;`"/> is true, `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> increases and `onUpdateHealthDisplay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:30:5:5:`        public Action onUpdateHealthDisplay;`"/> is invoked.
    

`UpdateHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:60:5:5:`        public void UpdateHealth(float value)`"/> - takes in a float (`value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:60:9:9:`        public void UpdateHealth(float value)`"/>)

*   increase `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> by `value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:60:9:9:`        public void UpdateHealth(float value)`"/>
    
*   if `value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:60:9:9:`        public void UpdateHealth(float value)`"/> is less than 0:
    
    *   set `_canRegen`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:26:5:5:`        private bool _canRegen;`"/> to false
        
    *   restart the `HealthRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:75:5:5:`        private IEnumerator HealthRegenDelay()`"/> coroutine
        
    *   if `_currentHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`"/> is less than 0, invoke `onDeath`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:29:5:5:`        public Action onDeath;`"/>
        
*   invoke `onUpdateHealthDisplay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:30:5:5:`        public Action onUpdateHealthDisplay;`"/>
    

`UpdateMaxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:73:5:5:`        public void UpdateMaxHealth(float value) =&gt; maxHealth += value;`"/> - takes in a float (`value`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:73:9:9:`        public void UpdateMaxHealth(float value) =&gt; maxHealth += value;`"/>) and increases `maxHealth`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`"/> by that value.

`HealthRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:75:5:5:`        private IEnumerator HealthRegenDelay()`"/> - waits for `healthRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:23:31:31:`        [SerializeField, Tooltip(&quot;Delay after taking damage before health will regenerate&quot;)] private float healthRegenDelay = 2.5f;`"/> seconds and then sets `_canRegen`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:26:5:5:`        private bool _canRegen;`"/> to true.

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/5v1da).
