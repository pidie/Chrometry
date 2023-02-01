---
id: gkp4s
title: Gun
file_version: 1.1.1
app_version: 1.1.0
---

# Overview

The `Gun`<swm-token data-swm-token=":Assets/TooManyCrosshairs/HELLSLAYER Crosshairs/_Demo/Scripts/Gun.cs:7:5:5:`    public class Gun : MonoBehaviour`"/>script is responsible for instantiating projectiles. The gun uses data from a `GunMod`<swm-token data-swm-token=":Assets/Data/Scripts/GunMod.cs:7:5:5:`    public class GunMod : ScriptableObject`"/>script to build the projectile and send it on its way.

## Of Note

*   The secondary functionality is called through the `Gun`<swm-token data-swm-token=":Assets/TooManyCrosshairs/HELLSLAYER Crosshairs/_Demo/Scripts/Gun.cs:7:5:5:`    public class Gun : MonoBehaviour`"/> script, but its actual code is handled in a separate script called `GunModSecondaryManager`<swm-token data-swm-token=":Assets/Scripts/Managers/GunModSecondaryManager.cs:8:5:5:`    public class GunModSecondaryManager : MonoBehaviour`"/>.
    
*   These methods are assigned via the inspector and named explicitly with the name of the gun mod and the callback context required for execution.
    

# Fields and Properties

<br/>

Serialized Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
13             [SerializeField] private GunMod gunMod;
14             [SerializeField] private GameObject muzzle;
```

<br/>

`gunMod`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:13:9:9:`        [SerializeField] private GunMod gunMod;`"/> - a container storing data about the gun mod<br/>
`muzzle`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:14:9:9:`        [SerializeField] private GameObject muzzle;`"/>\- the position where projectiles will be instantiated

<br/>

Private Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
16             private bool _canFire;
17             private bool _fireRequested;
18             private Vector3 _baseMuzzlePosition;
```

<br/>

`_canFire`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:16:5:5:`        private bool _canFire;`"/> - controls the rate of fire<br/>
`_fireRequested`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:17:5:5:`        private bool _fireRequested;`"/> - assigned true when the player presses the fire button<br/>
`_baseMuzzlePosition`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:18:5:5:`        private Vector3 _baseMuzzlePosition;`"/> - the initial muzzle position without any gun mods

*   As the player switches gun mods, the `muzzle`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:14:9:9:`        [SerializeField] private GameObject muzzle;`"/> position will change. The `_baseMuzzlePosition`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:18:5:5:`        private Vector3 _baseMuzzlePosition;`"/> is therefore a required grounding point so the next `gunMod`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:13:9:9:`        [SerializeField] private GunMod gunMod;`"/> attached affixes itself in the right place.

<br/>

Public Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
20             public GunStats gunStats;
```

<br/>

`gunStats`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:20:5:5:`        public GunStats gunStats;`"/> - a container for data from the `gunMod`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:13:9:9:`        [SerializeField] private GunMod gunMod;`"/>that can be manipulated.

<br/>

Structs
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
124            public struct GunStats
125            {
126                public float damageMin;
127                public float damageMax;
128                public float critChance;
129                public float critDamageMultiplier;
130                public bool isAutomatic;
131                public float timeBetweenShots;
132                public float range;
133                public float projectileSpeed;
134                public ProjectileData projectile;
135                public GameObject model;
136    
137                public GunStats(float damageMin, float damageMax, float critChance, float critDamageMultiplier, bool isAutomatic, 
138                    float timeBetweenShots, float range, float projectileSpeed, ProjectileData projectile, GameObject model)
139                {
140                    this.damageMin = damageMin;
141                    this.damageMax = damageMax;
142                    this.critChance = critChance;
143                    this.critDamageMultiplier = critDamageMultiplier;
144                    this.isAutomatic = isAutomatic;
145                    this.timeBetweenShots = timeBetweenShots;
146                    this.range = range;
147                    this.projectileSpeed = projectileSpeed;
148                    this.projectile = projectile;
149                    this.model = model;
150                }
151            }
```

<br/>

`GunStats`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:20:3:3:`        public GunStats gunStats;`"/> - creates a copy of the data from the `gunMod`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:13:9:9:`        [SerializeField] private GunMod gunMod;`"/> so it can be manipulated without damaging the initial data

# Methods

<br/>

Sets the initial muzzle position and assigns the default gun mod to the gun.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
22             private void Awake()
23             {
24                 _baseMuzzlePosition = muzzle.transform.position;
25                 SetGunMod(gunMod);
26             }
```

<br/>

<br/>

<br/>

Checks to see if the player fired the gun and prevents the weapon from autofiring if it's not automatic.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
28             private void Update()
29             {
30                 if (_fireRequested)
31                 {
32                     Fire();
33     
34                     if (!gunStats.isAutomatic)
35                         _fireRequested = false;
36                 }
37             }
```

<br/>

<br/>

<br/>

Called by the player directly to assign the value of `_fireRequested`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:17:5:5:`        private bool _fireRequested;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
39             public void FireRequest(InputAction.CallbackContext ctx) => _fireRequested = !ctx.canceled;
```

<br/>

<br/>

<br/>

Creates a projectile with necessary data and determines its initial direction.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
41             private void Fire()
42             {
43                 if (!_canFire) { }
44                 else
45                 {
46                     _canFire = false;
47                     StartCoroutine(GunFireCooldown());
48     
49                     // check to see if the projectile will hit something
50                     Vector3 direction;
51                     UnityEngine.Physics.Raycast(transform.position, transform.forward, out var hit, gunMod.range * 5);
52     
53                     // the projectile will either fly straight for its range or will fly towards the first target in range
54                     if (hit.point != Vector3.zero)
55                         direction = hit.point - muzzle.transform.position;
56                     else
57                         direction = transform.position + transform.forward * gunMod.range - muzzle.transform.position;
58     
59                     direction.Normalize();
60     
61                     // create the projectile
62                     GameObject projectileGo;
63                     Projectile projectile;
64     
65                     if (gunStats.critChance >= Random.Range(0f, 100f))
66                     {
67                         projectileGo = Instantiate(gunMod.projectile.critModel, muzzle.transform.position,
68                             quaternion.identity);
69                         projectile = projectileGo.GetComponent<Projectile>();
70                         projectile.WillCriticallyHit = true;
71                     }
72                     else
73                     {
74                         projectileGo = Instantiate(gunMod.projectile.baseModel, muzzle.transform.position,
75                             quaternion.identity);
76                         projectile = projectileGo.GetComponent<Projectile>();
77                     }
78     
79                     projectileGo.transform.rotation = transform.rotation;
80     
81                     // store data in the projectile
82                     projectile.MaxDistance = gunMod.range;
83                     projectile.ProjectileSpeed = gunMod.projectileSpeed;
84                     projectile.Direction = direction;
85                     projectile.CritDamageMultiplier = gunStats.critDamageMultiplier;
86     
87                     var damage = Random.Range(gunMod.damageMin, gunMod.damageMax);
88                     projectile.Damage = projectile.WillCriticallyHit ? damage * gunStats.critDamageMultiplier : damage;
89                 }
90             }
```

<br/>

<br/>

<br/>

Calls the necessary methods for the gun mod's secondary actions.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
92             public void Secondary(InputAction.CallbackContext ctx)
93             {
94                 if (ctx.started)
95                     gunMod.onStarted?.Invoke();
96                 else if (ctx.performed)
97                     gunMod.onPerformed?.Invoke();
98                 else if (ctx.canceled)
99                     gunMod.onCanceled?.Invoke();
100                else
101                    Debug.LogError($"Callback context could not be handled ({ctx})");
102            }
```

<br/>

<br/>

<br/>

Loads data from the gun mod into the `gunStats`<swm-token data-swm-token=":Assets/Scripts/Weapons/Gun.cs:107:1:1:`            gunStats = new GunStats(gunMod.damageMin, gunMod.damageMax, gunMod.critChance, gunMod.critDamageMultiplier, `"/> struct.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
104            private void SetGunMod(GunMod mod)
105            {
106                gunMod = mod;
107                gunStats = new GunStats(gunMod.damageMin, gunMod.damageMax, gunMod.critChance, gunMod.critDamageMultiplier, 
108                    gunMod.isAutomatic, gunMod.timeBetweenShots, gunMod.range, gunMod.projectileSpeed, gunMod.projectile, gunMod.model);
109                
110                var modGo = Instantiate(gunMod.model, _baseMuzzlePosition, Quaternion.identity, transform);
111                var modController = modGo.GetComponent<GunModModelController>();
112                muzzle.transform.position = modController.GetMuzzlePosition();
113                
114                StopAllCoroutines();
115                _canFire = true;
116            }
```

<br/>

<br/>

<br/>

Waits the specified amount of time before enabling the gun to be fired again.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Weapons/Gun.cs
```c#
118            private IEnumerator GunFireCooldown()
119            {
120                yield return new WaitForSeconds(gunStats.timeBetweenShots);
121                _canFire = true;
122            }
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/gkp4s).
