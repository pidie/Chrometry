---
id: 7b6po
title: Dealing Damage
file_version: 1.1.1
app_version: 1.1.4
---

# Overview

This article will go through the process of how damage is dealt in Chrometry. It is a process that involves a few different scripts working together, including `IDamager`<swm-token data-swm-token=":Assets/Scripts/Interfaces/IDamager.cs:3:5:5:`	public interface IDamager`"/>, `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:7:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>, and `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/>.

In addition to the above, this article will go through specifically how the player deals damage to opponents with the gun, which will also use the `Gun`<swm-token data-swm-token=":Assets/TooManyCrosshairs/_Demo/Scripts/Gun.cs:7:5:5:`    public class Gun : MonoBehaviour`"/>, `GunMod`<swm-token data-swm-token=":Assets/Data/Scripts/GunMod.cs:7:5:5:`    public class GunMod : ScriptableObject`"/>, and `Projectile`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/Projectile.cs:6:5:5:`    public class Projectile : MonoBehaviour, IDamager`"/> classes.

# Damage Basics

The `IDamager`<swm-token data-swm-token=":Assets/Scripts/Interfaces/IDamager.cs:3:5:5:`	public interface IDamager`"/> interface is inherited by any class that will deal damage. Currently, those classes are the `Projectile`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/Projectile.cs:6:5:5:`    public class Projectile : MonoBehaviour, IDamager`"/>, the `ExplosionController`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/ExplosionController.cs:7:5:5:`    public class ExplosionController : MonoBehaviour, IDamager`"/>, and the `DamageZoneController`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/DamageZoneController.cs:7:5:5:`    public class DamageZoneController : MonoBehaviour, IDamager`"/>. Each object with one of these components also has a `Collider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:28:7:7:`        protected void OnTriggerEnter(Collider other)`"/> component.

While instantiated, there is a chance that the collider from the damager will interact with the collider of an object with a `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:7:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>. During this interaction, two methods within the vitals collider controller will trigger the damage to be dealt:

<br/>

<br/>

The vitals collider detects a collision involving an `IDamager`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:30:11:11:`            var damager = other.GetComponent&lt;IDamager&gt;();`"/>. **Line 36** submits a damage report.
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
```

<br/>

The damage is calculated and reported to the `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/>.
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

<br/>

# Dealing Damage via Guns

The key call outs when thinking about damage via guns is how the damage is relayed to the `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:7:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>. Guns will always use a `Projectile`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/Projectile.cs:6:5:5:`    public class Projectile : MonoBehaviour, IDamager`"/>, a type of `IDamager`<swm-token data-swm-token=":Assets/Scripts/Interfaces/IDamager.cs:3:5:5:`	public interface IDamager`"/>.

The projectile stores information about how it is supposed to behave and interact with other game objects, including damage.

<br/>

Fields and properties stored within the `Projectile`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/Projectile.cs:6:5:5:`    public class Projectile : MonoBehaviour, IDamager`"/>
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Damagers/Projectile.cs
```c#
8              public float MaxDistance { get; set; }
9              public float ProjectileSpeed { get; set; }
10             public Vector3 Direction { get; set; }
11             public float Damage { get; set; }
12             public bool WillCriticallyHit { get; set; }
13             public float CritDamageMultiplier { get; set; }
14     
15             private Vector3 _initialPosition;
```

<br/>

When a gun is fired, a projectile is instantiated.

<br/>

An excerpt from one path in which a projectile is instantiated.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
74                         projectileGo = Instantiate(gunStats.projectile.baseModel, muzzle.transform.position,
```

<br/>

Then, the relevant information is stored within it.

<br/>

The `Projectile`<swm-token data-swm-token=":Assets/Scripts/Weapons/Damagers/Projectile.cs:6:5:5:`    public class Projectile : MonoBehaviour, IDamager`"/> component attached to the instantiated game object is loaded with data.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
82                     projectile.MaxDistance = gunStats.range;
83                     projectile.ProjectileSpeed = gunStats.projectileSpeed;
84                     projectile.Direction = direction;
85                     projectile.CritDamageMultiplier = gunStats.critDamageMultiplier;
86     
87                     var damage = Random.Range(gunStats.damageMin, gunStats.damageMax);
88                     projectile.Damage = projectile.WillCriticallyHit ? damage * gunStats.critDamageMultiplier : damage;
```

<br/>

From there, the projectile heads off in the direction dictated by the gun until it either destroys itself, or collides with a `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:7:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>, thus dealing damage.

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/7b6po).
