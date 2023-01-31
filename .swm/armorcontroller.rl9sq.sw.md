---
id: rl9sq
title: ArmorController
file_version: 1.1.1
app_version: 1.0.20
---

# Overview

The `ArmorController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:5:5:5:`    public class ArmorController : VitalsController`"/> inherits from the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/>, so much of its functionality is found there. Its special characteristics revolve around damage mitigation and tracking priority of damage.

## Of Note

*   _Armor_ should take damage before _Health_, but not before _Shields_.
    
*   Armor should help to ensure that proper damage order is being maintained.
    
*   Armor is the only vitals metric that does not regenerate over time.
    

# Fields and Properties

<br/>

Serialized Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/ArmorController.cs
```c#
7              [SerializeField] private float damagePercentageMitigated = 0.3f;
```

<br/>

`damagePercentageMitigated`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:7:9:9:`        [SerializeField] private float damagePercentageMitigated = 0.3f;`"/> - represents the amount of damage that is mitigated by armor

<br/>

Private Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/ArmorController.cs
```c#
9              private HealthController _healthController;
10             private ShieldController _shieldController;
11             private float _damagePercentageMitigatedInverse;
```

<br/>

`_healthController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:9:5:5:`        private HealthController _healthController;`"/> - a reference to a `HealthController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:9:3:3:`        private HealthController _healthController;`"/><br/>
`_shieldController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:10:5:5:`        private ShieldController _shieldController;`"/> - a reference to a `ShieldController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:10:3:3:`        private ShieldController _shieldController;`"/><br/>
`_damagePercentageMitigatedInverse`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:11:5:5:`        private float _damagePercentageMitigatedInverse;`"/> - a value used in calculating damage that gets passed to `_healthController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:9:5:5:`        private HealthController _healthController;`"/>

# Methods

<br/>

saves a reference to the `HealthController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:15:7:7:`            _healthController = GetComponent&lt;HealthController&gt;();`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/ArmorController.cs
```c#
13             protected override void Awake()
14             {
15                 _healthController = GetComponent<HealthController>();
16                 _shieldController = GetComponent<ShieldController>();
17                 _damagePercentageMitigatedInverse = 1 / (1 - damagePercentageMitigated);
18                 base.Awake();
19             }
```

<br/>

Updates the value based on the mitigated damage, passing leftover damage to the `_healthController`<swm-token data-swm-token=":Assets/Scripts/Vitals/ArmorController.cs:49:1:1:`                    _healthController.onToggleCollider(false);`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/ArmorController.cs
```c#
21             public override void UpdateValue(float value)
22             {
23                 if (!QueryColliderIsEnabled() && value < 0) return;
24                 
25                 var shieldIsActive = _shieldController.QueryColliderIsEnabled();
26                 
27                 if (value > 0)
28                 {
29                     if (!shieldIsActive)
30                         onToggleCollider.Invoke(true);
31     
32                     currentValue += value;
33                 }
34                 else
35                 {
36                     if (_shieldController != null && _shieldController.HasShieldGenerator)
37                         _shieldController.onRestartShieldRegenerationDelay?.Invoke();
38                         
39                     var damageToHealth = MitigateDamage(value, out var hasLeftoverDamage);
40     
41                     if (hasLeftoverDamage)
42                     {
43                         _healthController.onToggleCollider.Invoke(true);
44                         _healthController.UpdateValue(damageToHealth);
45                         onToggleCollider.Invoke(false);
46                     }
47                     else
48                     {
49                         _healthController.onToggleCollider(false);
50                     }
51                 }
52                 
53                 onUpdateDisplay?.Invoke();
54             }
```

<br/>

Calculates the amount of damage mitigated by armor.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/ArmorController.cs
```c#
56             public float MitigateDamage(float damage, out bool leftoverDamage)
57             {
58                 var mitigatedDamage = damage * (1 - damagePercentageMitigated);
59                 if (currentValue + mitigatedDamage < 0)
60                 {
61                     leftoverDamage = true;
62                     var damagePassedToHealth = damage + currentValue * _damagePercentageMitigatedInverse;
63                     currentValue = 0;
64                     return damagePassedToHealth;
65                 }
66                 
67                 leftoverDamage = false;
68                 currentValue += mitigatedDamage;
69                 return 0;
70             }
```

<br/>

Evaluates for the inverse of the percentage of damage mitigated by armor.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/ArmorController.cs
```c#
72             private void EvaluateDamagePercentMitigatedInverse() => _damagePercentageMitigatedInverse = 1 / (1 - damagePercentageMitigated);
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/rl9sq).
