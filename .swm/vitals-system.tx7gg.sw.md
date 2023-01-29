---
id: tx7gg
title: Vitals System
file_version: 1.1.1
app_version: 1.0.20
---

# Metrics

The vitals system is built out of classes that govern an entity's metrics. Currently, those metrics are _health_, _armor_, and _shields_, with _energy_ on the way.

*   **_Health_** is an entity's hit points. Once this value reaches 0, the entity is considered killed or destroyed. Health will usually regenerate slowly over time.
    
*   **_Armor_** is a barrier for health. Damage to armor is reduced by 30%. Armor does not regenerate.
    
*   **_Shields_** produce a protective field around an entity, taking damage before health or shields. They do however have an energy cost to maintain, and an increased energy cost when recharging. When unpowered, shields will slowly decay and take from 10% to 75% bonus damage, increasing as the shields value decreases.
    
*   **_Energy_** is used by entities to power their abilities. Energy will usually regenerate slowly over time. Most entities that use energy will have a battery that can store energy.
    

> _An entity is an actor or structure that makes use of vitals._

<br/>

# Components

The core of the vitals system is the VitalsController, an abstract class that stores and manipulates metric data. Each subclass is responsible for a single metric.

The VitalsColliderController is a component that requires a Collider. Once linked to a VitalsController, it detects any relevant collisions and calls the VitalsController to update its data. The VitalsColliderController takes a reference to a VitalsController via the Inspector, so these components do not need to be on the same game object. Each VitalsColliderController can only be hooked up to one VitalsController at a time, but a single VitalsController can have multiple VitalsColliderControllers hooked up to it.

The VitalsDisplayUI is an abstract class requiring an Image (from UnityEngine.UI). It controls the UI element responsible for its VitalsController's metric. Each subclass handles its metrics differently.

<br/>

# Old

~~The health system uses three main components - `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`"/>, `HealthColliderController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthColliderController.cs:8:5:5:`    public class HealthColliderController : MonoBehaviour`"/>, and `HealthDisplayUI`<swm-token data-swm-token=":Assets/Scripts/Health/HealthDisplayUI.cs:8:5:5:`    public class HealthDisplayUI : MonoBehaviour`"/>. All three classes are located at `📄 Assets/Scripts/Health`.

<br/>

<!--MERMAID {width:50}-->
```mermaid
\---
title: Dependency Chart
\---
classDiagram
`HealthController` <.. `HealthColliderController` : Dependency
`HealthController` <.. `HealthDisplayUI` : Dependency
<br/>`HealthController` : -float `maxHealth`
`HealthController` : -float `healthRegen`
`HealthController` : -float `healthRegenDelay`
`HealthController` : -float `_currentHealth`
`HealthController` : -bool `_canRegen`
`HealthController` : +Action `onDeath`
`HealthController` : +Action `onUpdateHealthDisplay`
<br/>`HealthController` : +`GetCurrentHealth` float
`HealthController` : +`GetMaxHealth` float
`HealthController` : +SetMaxHealth(float, bool = true)
`HealthController` : UpdateHealth(float)
`HealthController` : UpdateMaxHealth()
`HealthController` : -`HealthRegenDelay` `IEnumerator`
class HealthColliderController{
\-`HealthController` `healthController`
\-float `damageModifier`
<br/>\-OnTriggerEnter()
\-OnCollisionEnter()
\-HandleProjectileCollision()
}
class HealthDisplayUI{
\-`HealthController` `healthController`
\-TMP\_Text \_text
<br/>\-OnEnable()
\-OnDisable()
\-UpdateHealthDisplay()
}
```
<!--MCONTENT {content: "\\---<br/>\ntitle: Dependency Chart<br/>\n\\---<br/>\nclassDiagram<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> <.. `HealthColliderController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:8:5:5:`    public class HealthColliderController : MonoBehaviour`\"/> : Dependency<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> <.. `HealthDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:5:5:`    public class HealthDisplayUI : MonoBehaviour`\"/> : Dependency<br/>\n<br/>`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `maxHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:10:9:9:`        [SerializeField] private float maxHealth = 100f;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `healthRegen`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:12:23:23:`        [SerializeField, Tooltip(&quot;Health regenerated per second&quot;)] private float healthRegen = 1.5f;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `healthRegenDelay`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:13:31:31:`        [SerializeField, Tooltip(&quot;Delay after taking damage before health will regenerate&quot;)] private float healthRegenDelay = 2.5f;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `_currentHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:15:5:5:`        private float _currentHealth;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : -bool `_canRegen`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:16:5:5:`        private bool _canRegen;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : +Action `onDeath`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:19:5:5:`        public Action onDeath;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`\"/> : +Action `onUpdateHealthDisplay`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:20:5:5:`        public Action onUpdateHealthDisplay;`\"/><br/>\n<br/>`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> : +`GetCurrentHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:22:5:5:`        public float GetCurrentHealth() =&gt; _currentHealth;`\"/> float<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> : +`GetMaxHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:24:5:5:`        public float GetMaxHealth() =&gt; maxHealth;`\"/> float<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> : +SetMaxHealth(float, bool = true)<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> : UpdateHealth(float)<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> : UpdateMaxHealth()<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> : -`HealthRegenDelay`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:68:5:5:`        private IEnumerator HealthRegenDelay()`\"/> `IEnumerator`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:68:3:3:`        private IEnumerator HealthRegenDelay()`\"/><br/>\nclass HealthColliderController{<br/>\n\\-`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> `healthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:36:1:1:`            healthController.UpdateHealth(damager.Damage * -1f * damageModifier);`\"/><br/>\n\\-float `damageModifier`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:36:16:16:`            healthController.UpdateHealth(damager.Damage * -1f * damageModifier);`\"/><br/>\n<br/>\\-OnTriggerEnter()<br/>\n\\-OnCollisionEnter()<br/>\n\\-HandleProjectileCollision()<br/>\n}<br/>\nclass HealthDisplayUI{<br/>\n\\-`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:10:7:7:`        [SerializeField] private HealthController healthController;`\"/> `healthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:36:1:1:`            healthController.UpdateHealth(damager.Damage * -1f * damageModifier);`\"/><br/>\n\\-TMP\\_Text \\_text<br/>\n<br/>\\-OnEnable()<br/>\n\\-OnDisable()<br/>\n\\-UpdateHealthDisplay()<br/>\n}"} --->

<br/>

# [HealthController](healthcontroller.5v1da.sw.md)

The `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`"/> is the brain of the health system. There should only be one `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`"/> for any entity with a trackable health.<br/>
<br/>

# [HealthColliderController](healthcollidercontroller.aceyz.sw.md)

The `HealthColliderController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthColliderController.cs:8:5:5:`    public class HealthColliderController : MonoBehaviour`"/> detects collisions that could affect the `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`"/>. It references a `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:7:5:5:`    public class HealthController : MonoBehaviour`"/> and activates events for it.<br/>
<br/>

# HealthDisplayUI

The `HealthDisplayUI`<swm-token data-swm-token=":Assets/Scripts/Health/HealthDisplayUI.cs:8:5:5:`    public class HealthDisplayUI : MonoBehaviour`"/> is used to interact with a user interface for displaying health information.

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/tx7gg).
