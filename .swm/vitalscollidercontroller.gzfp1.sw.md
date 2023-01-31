---
id: gzfp1
title: VitalsColliderController
file_version: 1.1.1
app_version: 1.0.20
---

# Overview

This component utilizes a connected `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:6:6:`    [RequireComponent(typeof(Collider))]`"/> to register events that would affect a vital metric. It then sends instructions to the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> it is registered to as to how to handle the collision.

## Before you continue

*   The script requires a `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:6:6:`    [RequireComponent(typeof(Collider))]`"/> on the `GameObject` in order to be attached. Since `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:6:6:`    [RequireComponent(typeof(Collider))]`"/> is a base class, Unity _will not_ automatically attach one to it. Instead, the `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:6:6:`    [RequireComponent(typeof(Collider))]`"/> should be set up before this script is attached.

<br/>

This component requires a `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:6:6:`    [RequireComponent(typeof(Collider))]`"/> be attached to the `GameObject`.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
6          [RequireComponent(typeof(Collider))]
```

<br/>

*   The `IDamager`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:79:7:7:`        protected void HandleIDamagerCollision(IDamager damager)`"/> type referenced some methods is an interface that is inherited by any class that can deal damage.
    

# Fields and Properties

<br/>

Serialized Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
9              [SerializeField] protected float damageMultiplier = 1f;
10             [SerializeField] protected VitalsController vitalsController;
```

<br/>

`damageMultiplier`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:9:9:9:`        [SerializeField] protected float damageMultiplier = 1f;`"/> - multiply damage by this amount<br/>
`VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:10:7:7:`        [SerializeField] protected VitalsController vitalsController;`"/> - the controller this component is registered to

<br/>

Protected Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
12             protected Collider _collider;
```

<br/>

`_collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:12:5:5:`        protected Collider _collider;`"/> - a reference to the attached `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:12:3:3:`        protected Collider _collider;`"/>

<br/>

Public Properties
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
14             public float TimeSinceLastCollision { get; private set; }
```

<br/>

`TimeSinceLastCollision`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:14:5:5:`        public float TimeSinceLastCollision { get; private set; }`"/> - the amount of time that has passed since the attached `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:6:6:`    [RequireComponent(typeof(Collider))]`"/> registered a collision

# Methods

<br/>

Assigns the `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:18:7:7:`            _collider = GetComponent&lt;Collider&gt;();`"/> reference an registers this to the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:10:7:7:`        [SerializeField] protected VitalsController vitalsController;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
16             protected void Awake()
17             {
18                 _collider = GetComponent<Collider>();
19                 vitalsController.RegisterColliderToController(this);
20             }
```

<br/>

Updates `TimeSinceLastCollision`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:22:11:11:`        protected void Update() =&gt; TimeSinceLastCollision += Time.deltaTime;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
22             protected void Update() => TimeSinceLastCollision += Time.deltaTime;
```

<br/>

Register and deregister the `ToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:72:5:5:`        protected void ToggleCollider(bool value)`"/> method to the `onToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:24:13:13:`        protected void OnEnable() =&gt; vitalsController.onToggleCollider += ToggleCollider;`"/> Action.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
24             protected void OnEnable() => vitalsController.onToggleCollider += ToggleCollider;
25     
26             protected void OnDisable() => vitalsController.onToggleCollider -= ToggleCollider;
```

<br/>

Handle detected collisions, looking for incoming damage. Both `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:28:7:7:`        protected void OnTriggerEnter(Collider other)`"/> and `Collision`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:40:7:7:`        protected void OnCollisionEnter(Collision other)`"/> types are evaluated for so we can introduce non-projectile based damage sources, such as explosion waves or heat damage, which would be _triggers_ instead of _collisions_.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
28             protected void OnTriggerEnter(Collider other)
29             {
30                 var damager = other.GetComponent<IDamager>();
31     
32                 if (damager != null)
33                 {
34                     damager.SetIsInDamagerCollider(true);
35                     TimeSinceLastCollision = 0;
36                     HandleIDamagerCollision(damager);
37                 }
38             }
39     
40             protected void OnCollisionEnter(Collision other)
41             {
42                 var damager = other.gameObject.GetComponent<IDamager>();
43     
44                 if (damager != null)
45                 {
46                     damager.SetIsInDamagerCollider(true);
47                     TimeSinceLastCollision = 0;
48                     HandleIDamagerCollision(damager);
49                 }
50             }
```

<br/>

Sets a bool for areas that deal persistent damage to false.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
52             protected void OnTriggerExit(Collider other)
53             {
54                 var damager = other.GetComponent<IDamager>();
55     
56                 if (damager != null)
57                 {
58                     damager.SetIsInDamagerCollider(false);
59                 }
60             }
61     
62             protected void OnCollisionExit(Collision other)
63             {
64                 var damager = other.gameObject.GetComponent<IDamager>();
65     
66                 if (damager != null)
67                 {
68                     damager.SetIsInDamagerCollider(false);
69                 }
70             }
```

<br/>

Enables or disables the `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:12:3:3:`        protected Collider _collider;`"/>, allowing or preventing collision detection.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
72             protected void ToggleCollider(bool value)
73             {
74                 _collider.enabled = value;
75                 vitalsController.SetColliderEnabled(this);
76             }
```

<br/>

Sends information to the `vitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:86:1:1:`            vitalsController.UpdateValue(damage * -1f * damageMultiplier);`"/> as to how to handle the incoming damage, and then calls the `IDamager`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:79:7:7:`        protected void HandleIDamagerCollision(IDamager damager)`"/> to handle its own collision.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
79             protected void HandleIDamagerCollision(IDamager damager)
80             {
81                 var damage = damager.Damage;
82                 
83                 if (damager.WillCriticallyHit)
84                     damage *= damager.CritDamageMultiplier;
85                 
86                 vitalsController.UpdateValue(damage * -1f * damageMultiplier);
87                 damager.CollideWithObject();
88             }
```

<br/>

An accessor method, allowing the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:10:7:7:`        [SerializeField] protected VitalsController vitalsController;`"/> to manually check the enabled state of this `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:12:3:3:`        protected Collider _collider;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
90             public bool GetColliderEnabledState() => _collider.enabled;
```

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/gzfp1).
