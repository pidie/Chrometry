---
id: tx7gg
title: Health System
file_version: 1.1.1
app_version: 1.0.20
---

The health system uses three main components - `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/>, `HealthColliderController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthColliderController.cs:6:5:5:`    public class HealthColliderController : MonoBehaviour`"/>, and `HealthDisplayUI`<swm-token data-swm-token=":Assets/Scripts/Health/HealthDisplayUI.cs:6:5:5:`    public class HealthDisplayUI : MonoBehaviour`"/>. All three classes are located at `📄 Assets/Scripts/Health`.

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
<br/>`HealthController` : +`GetHealth` float
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
<!--MCONTENT {content: "\\---<br/>\ntitle: Dependency Chart<br/>\n\\---<br/>\nclassDiagram<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> <.. `HealthColliderController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:6:5:5:`    public class HealthColliderController : MonoBehaviour`\"/> : Dependency<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> <.. `HealthDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:6:5:5:`    public class HealthDisplayUI : MonoBehaviour`\"/> : Dependency<br/>\n<br/>`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `maxHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:21:9:9:`        [SerializeField] private float maxHealth = 100f;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `healthRegen`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:22:23:23:`        [SerializeField, Tooltip(&quot;Health regenerated per second&quot;)] private float healthRegen = 1.5f;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `healthRegenDelay`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:23:31:31:`        [SerializeField, Tooltip(&quot;Delay after taking damage before health will regenerate&quot;)] private float healthRegenDelay = 2.5f;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : -float `_currentHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:25:5:5:`        private float _currentHealth;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : -bool `_canRegen`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:26:5:5:`        private bool _canRegen;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : +Action `onDeath`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:29:5:5:`        public Action onDeath;`\"/><br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`\"/> : +Action `onUpdateHealthDisplay`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:30:5:5:`        public Action onUpdateHealthDisplay;`\"/><br/>\n<br/>`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> : +`GetHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:32:5:5:`        public float GetHealth() =&gt; _currentHealth;`\"/> float<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> : +`GetMaxHealth`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:34:5:5:`        public float GetMaxHealth() =&gt; maxHealth;`\"/> float<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> : +SetMaxHealth(float, bool = true)<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> : UpdateHealth(float)<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> : UpdateMaxHealth()<br/>\n`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> : -`HealthRegenDelay`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:75:5:5:`        private IEnumerator HealthRegenDelay()`\"/> `IEnumerator`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthController.cs:75:3:3:`        private IEnumerator HealthRegenDelay()`\"/><br/>\nclass HealthColliderController{<br/>\n\\-`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> `healthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:28:1:1:`            healthController.UpdateHealth(projectile.Damage * -1f * damageModifier);`\"/><br/>\n\\-float `damageModifier`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:28:16:16:`            healthController.UpdateHealth(projectile.Damage * -1f * damageModifier);`\"/><br/>\n<br/>\\-OnTriggerEnter()<br/>\n\\-OnCollisionEnter()<br/>\n\\-HandleProjectileCollision()<br/>\n}<br/>\nclass HealthDisplayUI{<br/>\n\\-`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthDisplayUI.cs:8:7:7:`        [SerializeField] private HealthController healthController;`\"/> `healthController`<swm-token data-swm-token=\":Assets/Scripts/Health/HealthColliderController.cs:28:1:1:`            healthController.UpdateHealth(projectile.Damage * -1f * damageModifier);`\"/><br/>\n\\-TMP\\_Text \\_text<br/>\n<br/>\\-OnEnable()<br/>\n\\-OnDisable()<br/>\n\\-UpdateHealthDisplay()<br/>\n}"} --->

<br/>

# [HealthController](healthcontroller.5v1da.sw.md)

The `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/> is the brain of the health system. There should only be one `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/> for any entity with a trackable health.<br/>
<br/>

# [HealthColliderController](healthcollidercontroller.aceyz.sw.md)

The `HealthColliderController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthColliderController.cs:6:5:5:`    public class HealthColliderController : MonoBehaviour`"/> detects collisions that could affect the `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/>. It references a `HealthController`<swm-token data-swm-token=":Assets/Scripts/Health/HealthController.cs:18:5:5:`    public class HealthController : MonoBehaviour`"/> and activates events for it.<br/>
<br/>

# HealthDisplayUI

The `HealthDisplayUI`<swm-token data-swm-token=":Assets/Scripts/Health/HealthDisplayUI.cs:6:5:5:`    public class HealthDisplayUI : MonoBehaviour`"/> is used to interact with a user interface for displaying health information.

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/tx7gg).
