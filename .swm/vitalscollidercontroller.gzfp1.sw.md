---
id: gzfp1
title: VitalsColliderController
file_version: 1.1.1
app_version: 1.0.20
---

# Overview

This component utilizes a connected `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:5:6:6:`    [RequireComponent(typeof(Collider))]`"/> to register events that would affect a vital metric. It then sends instructions to the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> it is registered to as to how to handle the collision.

<br/>

## Before you continue

*   The script requires a `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:5:6:6:`    [RequireComponent(typeof(Collider))]`"/> on the `GameObject` in order to be attached. Since `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:5:6:6:`    [RequireComponent(typeof(Collider))]`"/> is a base class, Unity _will not_ automatically attach one to it. Instead, the `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:5:6:6:`    [RequireComponent(typeof(Collider))]`"/> should be set up before this script is attached.

<br/>

This component requires a `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:5:6:6:`    [RequireComponent(typeof(Collider))]`"/> be attached to the `GameObject`.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
5          [RequireComponent(typeof(Collider))]
```

<br/>

*   The `IDamager`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:56:9:9:`        protected void HandleIDamagerCollision(Interfaces.IDamager damager)`"/> type referenced some methods is an interface that is inherited by any class that can deal damage. Currently, the only type that can deal damage is `Projectile`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:29:15:15:`            var projectile = other.gameObject.GetComponent&lt;Weapons.Projectile&gt;();`"/>, but there are plans to include more. The `IDamager`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:56:9:9:`        protected void HandleIDamagerCollision(Interfaces.IDamager damager)`"/> is at the least a temporary placeholder for a parent type for dealing damage.
    

<br/>

# Fields and Properties

<br/>

Serialized Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
8              [SerializeField] protected float damageMultiplier = 1f;
9              [SerializeField] protected VitalsController vitalsController;
```

<br/>

`damageMultiplier`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:8:9:9:`        [SerializeField] protected float damageMultiplier = 1f;`"/> - multiply damage by this amount<br/>
`VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:9:7:7:`        [SerializeField] protected VitalsController vitalsController;`"/> - the controller this component is registered to

<br/>

<br/>

Protected Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
11             protected Collider _collider;
```

<br/>

`_collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:11:5:5:`        protected Collider _collider;`"/> - a reference to the attached `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:11:3:3:`        protected Collider _collider;`"/>

<br/>

<br/>

Public Properties
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
13             public float TimeSinceLastCollision { get; private set; }
```

<br/>

`TimeSinceLastCollision`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:13:5:5:`        public float TimeSinceLastCollision { get; private set; }`"/> - the amount of time that has passed since the attached `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:5:6:6:`    [RequireComponent(typeof(Collider))]`"/> registered a collision

<br/>

# Methods

<br/>

Assigns the `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:17:7:7:`            _collider = GetComponent&lt;Collider&gt;();`"/> reference an registers this to the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:9:7:7:`        [SerializeField] protected VitalsController vitalsController;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
15             protected void Awake()
16             {
17                 _collider = GetComponent<Collider>();
18                 vitalsController.RegisterColliderToController(this);
19             }
```

<br/>

Updates `TimeSinceLastCollision`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:21:11:11:`        protected void Update() =&gt; TimeSinceLastCollision += Time.deltaTime;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
21             protected void Update() => TimeSinceLastCollision += Time.deltaTime;
```

<br/>

Register and deregister the `ToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:49:5:5:`        protected void ToggleCollider(bool value)`"/> method to the `onToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:23:13:13:`        protected void OnEnable() =&gt; vitalsController.onToggleCollider += ToggleCollider;`"/> Action.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
23             protected void OnEnable() => vitalsController.onToggleCollider += ToggleCollider;
24     
25             protected void OnDisable() => vitalsController.onToggleCollider -= ToggleCollider;
```

<br/>

Handle detected collisions, looking for incoming damage. Both `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:27:7:7:`        protected void OnTriggerEnter(Collider other)`"/> and `Collision`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:38:7:7:`        protected void OnCollisionEnter(Collision collision)`"/> types are evaluated for so we can introduce non-projectile based damage sources, such as explosion waves or heat damage, which would be _triggers_ instead of _collisions_.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
27             protected void OnTriggerEnter(Collider other)
28             {
29                 var projectile = other.gameObject.GetComponent<Weapons.Projectile>();
30     
31                 if (projectile)
32                 {
33                     TimeSinceLastCollision = 0;
34                     HandleIDamagerCollision(projectile);
35                 }
36             }
37     
38             protected void OnCollisionEnter(Collision collision)
39             {
40                 var projectile = collision.gameObject.GetComponent<Weapons.Projectile>();
41     
42                 if (projectile)
43                 {
44                     TimeSinceLastCollision = 0;
45                     HandleIDamagerCollision(projectile);
46                 }
47             }
```

<br/>

Enables or disables the `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:11:3:3:`        protected Collider _collider;`"/>, allowing or preventing collision detection.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
49             protected void ToggleCollider(bool value)
50             {
51                 _collider.enabled = value;
52                 vitalsController.SetColliderEnabled(this);
53             }
```

<br/>

Sends information to the `vitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:58:1:1:`            vitalsController.UpdateValue(damager.Damage * -1f * damageMultiplier);`"/> as to how to handle the incoming damage, and then calls the `IDamager`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:56:9:9:`        protected void HandleIDamagerCollision(Interfaces.IDamager damager)`"/> to handle its own collision.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
56             protected void HandleIDamagerCollision(Interfaces.IDamager damager)
57             {
58                 vitalsController.UpdateValue(damager.Damage * -1f * damageMultiplier);
59                 damager.CollideWithObject();
60             }
```

<br/>

An accessor method, allowing the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:9:7:7:`        [SerializeField] protected VitalsController vitalsController;`"/> to manually check the enabled state of this `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:11:3:3:`        protected Collider _collider;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsColliderController.cs
```c#
62             public bool GetColliderEnabledState() => _collider.enabled;
```

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/gzfp1).
