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

# Components

The core of the vitals system is the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/>, an abstract class that stores and manipulates metric data. Each subclass is responsible for a single metric.

The `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/> is a component that requires a Collider. Once linked to a `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/>, it detects any relevant collisions and calls the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> to update its data. The `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/> takes a reference to a `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> via the Inspector, so these components do not need to be on the same game object. Each `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/> can only be hooked up to one `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> at a time, but a single `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> can have multiple VitalsColliderControllers hooked up to it.

The `VitalsDisplayUI`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsDisplayUI.cs:8:7:7:`	public abstract class VitalsDisplayUI : MonoBehaviour`"/> is an abstract class requiring an Image (from UnityEngine.UI). It controls the UI element responsible for its `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/>'s metric. Each subclass handles its metrics differently.

The dependency chart below gives a brief overview of the contents of each of these components and those that inherit from them. It should be noted that for the sake of this diagram, methods that inherit from UnityEngine have been omitted. A more thorough explanation will be included in each dedicated component page and will include the functionality of said omitted methods.

Click on a class to learn more.

<br/>

<!--MERMAID {width:100}-->
```mermaid
\---
title: Dependency Chart
\---
classDiagram
`VitalsController` <.. `VitalsColliderController` : Dependency
`VitalsController` <.. `VitalsDisplayUI` : Dependency
`VitalsController` <|-- `HealthController` : Inheritance
`VitalsController` <|-- `ArmorController` : Inheritance
`VitalsController` <|-- `ShieldController` : Inheritance
`VitalsDisplayUI` <|-- `HealthDisplayUI` : Inheritance
`VitalsDisplayUI` <|-- `ArmorShieldDisplayUI` : Inheritance
<<Abstract>> `VitalsController`
class `VitalsController`{
#float maxValue
#float currentValueOverride
#float vitalRegen
#float vitalRegenDelay
#bool canRegen
#List`VitalsColliderController` colliderControllers
#Listbool colliderEnabledStates
+Action onUpdateDisplay
+Actionbool onToggleCollider
+float CurrentValue
+float MaxValue
+SetMaxValue(float, bool = true, bool = false)
+UpdateValue(float)
+UpdateMaxValue(float)
#RegenDelay() IEnumerator
+RegisterColliderToController(`VitalsColliderController`)
+SetColliderEnabled(`VitalsColliderController`)
+QueryColliderIsEnabled() bool
+QueryLastCollision() float
#RestartRegenCountdown()
#RefreshColliderEnabledStates()
}
class `VitalsColliderController`{
#float damageMultiplier
#`VitalsController` vitalsController
#Collider \_collider
+float `TimeSinceLastCollision`
#ToggleCollider(bool)
#HandleIDamagerCollision(IDamager)
+GetColliderEnabledState() bool
}
<<Abstract>> `VitalsDisplayUI`
class `VitalsDisplayUI` {
#`VitalsController` vitalsController
#Color maxHealthColor
#Color minHealthColor
#TMP\_Text text
#Image iconPlayer
#Image icon
#UpdateDisplay()$
}
class `HealthController` {
+Action onDeath
+UpdateValue(float)$
}
class `ArmorController` {
\-float damageReduction
\-`HealthController` \_healthController\_
\_-float \_startingArmor
+UpdateValue(float)$
+SetStartingArmor(float)
}
class `ShieldController` {
\-`HealthController` \_healthController
\-`ArmorController` \_armorController
\-float \_startingShield
+UpdateValue(float)$
+SetStartingShield(float)
}
class `HealthDisplayUI`{
UpdateDisplay()$
}
class `ArmorShieldDisplayUI` {
`UpdateDisplay()`$
}
click `VitalsController` href "[https://app.swimm.io/workspaces/xk0cIoHAkoKXsgB3KRFl/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/branch/master/docs/5g4tm](https://app.swimm.io/workspaces/xk0cIoHAkoKXsgB3KRFl/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/branch/master/docs/5g4tm)" "VitalsController"
```
<!--MCONTENT {content: "\\---<br/>\ntitle: Dependency Chart<br/>\n\\---<br/>\nclassDiagram<br/>\n`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> <.. `VitalsColliderController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`\"/> : Dependency<br/>\n`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> <.. `VitalsDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsDisplayUI.cs:8:7:7:`\tpublic abstract class VitalsDisplayUI : MonoBehaviour`\"/> : Dependency<br/>\n`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> <|-- `HealthController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/HealthController.cs:5:5:5:`    public class HealthController : VitalsController`\"/> : Inheritance<br/>\n`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> <|-- `ArmorController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ArmorController.cs:5:5:5:`    public class ArmorController : VitalsController`\"/> : Inheritance<br/>\n`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> <|-- `ShieldController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ShieldController.cs:3:5:5:`    public class ShieldController : VitalsController`\"/> : Inheritance<br/>\n`VitalsDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsDisplayUI.cs:8:7:7:`\tpublic abstract class VitalsDisplayUI : MonoBehaviour`\"/> <|-- `HealthDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/HealthDisplayUI.cs:5:5:5:`    public class HealthDisplayUI : VitalsDisplayUI`\"/> : Inheritance<br/>\n`VitalsDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsDisplayUI.cs:8:7:7:`\tpublic abstract class VitalsDisplayUI : MonoBehaviour`\"/> <|-- `ArmorShieldDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ArmorShieldDisplayUI.cs:3:5:5:`\tpublic class ArmorShieldDisplayUI : VitalsDisplayUI`\"/> : Inheritance<br/>\n<<Abstract>> `VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/><br/>\nclass `VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/>{<br/>\n#float maxValue<br/>\n#float currentValueOverride<br/>\n#float vitalRegen<br/>\n#float vitalRegenDelay<br/>\n#bool canRegen<br/>\n#List`VitalsColliderController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`\"/> colliderControllers<br/>\n#Listbool colliderEnabledStates<br/>\n+Action onUpdateDisplay<br/>\n+Actionbool onToggleCollider<br/>\n+float CurrentValue<br/>\n+float MaxValue<br/>\n+SetMaxValue(float, bool = true, bool = false)<br/>\n+UpdateValue(float)<br/>\n+UpdateMaxValue(float)<br/>\n#RegenDelay() IEnumerator<br/>\n+RegisterColliderToController(`VitalsColliderController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`\"/>)<br/>\n+SetColliderEnabled(`VitalsColliderController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`\"/>)<br/>\n+QueryColliderIsEnabled() bool<br/>\n+QueryLastCollision() float<br/>\n#RestartRegenCountdown()<br/>\n#RefreshColliderEnabledStates()<br/>\n}<br/>\nclass `VitalsColliderController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`\"/>{<br/>\n#float damageMultiplier<br/>\n#`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> vitalsController<br/>\n#Collider \\_collider<br/>\n+float `TimeSinceLastCollision`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsColliderController.cs:13:5:5:`        public float TimeSinceLastCollision { get; private set; }`\"/><br/>\n#ToggleCollider(bool)<br/>\n#HandleIDamagerCollision(IDamager)<br/>\n+GetColliderEnabledState() bool<br/>\n}<br/>\n<<Abstract>> `VitalsDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsDisplayUI.cs:8:7:7:`\tpublic abstract class VitalsDisplayUI : MonoBehaviour`\"/><br/>\nclass `VitalsDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsDisplayUI.cs:8:7:7:`\tpublic abstract class VitalsDisplayUI : MonoBehaviour`\"/> {<br/>\n#`VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> vitalsController<br/>\n#Color maxHealthColor<br/>\n#Color minHealthColor<br/>\n#TMP\\_Text text<br/>\n#Image iconPlayer<br/>\n#Image icon<br/>\n#UpdateDisplay()$<br/>\n}<br/>\nclass `HealthController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/HealthController.cs:5:5:5:`    public class HealthController : VitalsController`\"/> {<br/>\n+Action onDeath<br/>\n+UpdateValue(float)$<br/>\n}<br/>\nclass `ArmorController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ArmorController.cs:5:5:5:`    public class ArmorController : VitalsController`\"/> {<br/>\n\\-float damageReduction<br/>\n\\-`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/HealthController.cs:5:5:5:`    public class HealthController : VitalsController`\"/> \\_healthController\\_<br/>\n\\_-float \\_startingArmor<br/>\n+UpdateValue(float)$<br/>\n+SetStartingArmor(float)<br/>\n}<br/>\nclass `ShieldController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ShieldController.cs:3:5:5:`    public class ShieldController : VitalsController`\"/> {<br/>\n\\-`HealthController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/HealthController.cs:5:5:5:`    public class HealthController : VitalsController`\"/> \\_healthController<br/>\n\\-`ArmorController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ArmorController.cs:5:5:5:`    public class ArmorController : VitalsController`\"/> \\_armorController<br/>\n\\-float \\_startingShield<br/>\n+UpdateValue(float)$<br/>\n+SetStartingShield(float)<br/>\n}<br/>\nclass `HealthDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/HealthDisplayUI.cs:5:5:5:`    public class HealthDisplayUI : VitalsDisplayUI`\"/>{<br/>\nUpdateDisplay()$<br/>\n}<br/>\nclass `ArmorShieldDisplayUI`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ArmorShieldDisplayUI.cs:3:5:5:`\tpublic class ArmorShieldDisplayUI : VitalsDisplayUI`\"/> {<br/>\n`UpdateDisplay()`<swm-token data-swm-token=\":Assets/Scripts/Vitals/ArmorShieldDisplayUI.cs:5:7:9:`\t\tprotected override void UpdateDisplay()`\"/>$<br/>\n}<br/>\nclick `VitalsController`<swm-token data-swm-token=\":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`\"/> href \"[https://app.swimm.io/workspaces/xk0cIoHAkoKXsgB3KRFl/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/branch/master/docs/5g4tm](https://app.swimm.io/workspaces/xk0cIoHAkoKXsgB3KRFl/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/branch/master/docs/5g4tm)\" \"VitalsController\""} --->

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/tx7gg).
