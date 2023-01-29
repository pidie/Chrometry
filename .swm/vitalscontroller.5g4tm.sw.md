---
id: 5g4tm
title: VitalsController
file_version: 1.1.1
app_version: 1.0.20
---

# Overview

The `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> is an abstract class for tracking a metric, with a subclass for each metric. All actors and structures that make use of vitals (collectively called _entities_) will have a component that inherits from this class.

## Purpose

The `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> grants essential functionality to a multitude of subclasses and dependent classes. It acts as an information hub, tracking and adjusting its values based on incoming information.

# Fields and Properties

<br/>

Serialized Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
11             [SerializeField] protected float maxValue;
12             [SerializeField] protected float currentValueOverride;
13             [SerializeField] protected float vitalRegen;
14             [SerializeField] protected float vitalRegenDelay;
```

<br/>

`maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> - the maximum value this metric can reach<br/>
`currentValueOverride`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:12:9:9:`        [SerializeField] protected float currentValueOverride;`"/> - starts the game with this metric at the given value<br/>
`vitalRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:13:9:9:`        [SerializeField] protected float vitalRegen;`"/> - how much of this metric you regain every second while regenerating<br/>
`vitalRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:14:9:9:`        [SerializeField] protected float vitalRegenDelay;`"/> - how many seconds after taking damage until regeneration can begin

<br/>

<br/>

Protected Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
16             protected float currentValue;
17             protected bool canRegen;
18             protected Dictionary<VitalsColliderController, bool> colliderControllers =
19                 new ();
```

<br/>

`currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> - the current value of the metric<br/>
`canRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:17:5:5:`        protected bool canRegen;`"/> - keeps track of whether or not this metric can regenerate<br/>
`colliderControllers`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:18:11:11:`        protected Dictionary&lt;VitalsColliderController, bool&gt; colliderControllers =`"/> - a collection of collider controllers that register to this controller and the enabled state of the collider components

<br/>

<br/>

Actions
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
21             public Action onUpdateDisplay;
22             public Action<bool> onToggleCollider;
```

<br/>

`onUpdateDisplay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:21:5:5:`        public Action onUpdateDisplay;`"/> - invoked when the UI display needs to be updated<br/>
`onToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:22:8:8:`        public Action&lt;bool&gt; onToggleCollider;`"/> - invoked when a registered collider should be toggled on or off - passing `true` enabled the collider

<br/>

<br/>

Public Accessors
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
24             public float CurrentValue => currentValue;
25             public float MaxValue => maxValue;
```

<br/>

<br/>

# Methods

Below, find each method and a brief explanation of what it does.

<br/>

Sets the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> to whatever the `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:27:9:9:`        public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)`"/> is and adjusts the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> as needed.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
27             public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)
28             {
29                 maxValue = value;
30     
31                 if (maxOutCurrentValue)
32                     currentValue = maxValue;
33                 else if (increaseCurrentValue)
34                     UpdateValue(value);
35             }
```

<br/>

Sets the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> and calls to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
37             protected virtual void Awake()
38             {
39                 currentValue = currentValueOverride < maxValue ? maxValue : currentValueOverride;
40                 onUpdateDisplay?.Invoke();
41             }
```

<br/>

Regenerates the metric and ensures it doesn't exceed the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> and calls to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
43             protected void Update()
44             {
45                 if (currentValue > maxValue) currentValue = maxValue;
46                 if (currentValue < maxValue && canRegen)
47                 {
48                     currentValue += vitalRegen * Time.deltaTime;
49                     onUpdateDisplay?.Invoke();
50                 }
51             }
```

<br/>

Adds the `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:53:11:11:`        public virtual void UpdateValue(float value)`"/> to the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/>, stopping regeneration if `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:53:11:11:`        public virtual void UpdateValue(float value)`"/> is negative and calling to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
53             public virtual void UpdateValue(float value)
54             {            
55                 currentValue += value;
56                 if (value < 0)
57                 {
58                     canRegen = false;
59                     StopAllCoroutines();
60                     StartCoroutine(RegenDelay());
61     
62                     RefreshColliderEnabledStates();
63                 }
64     
65                 onUpdateDisplay?.Invoke();
66             }
```

<br/>

Updates the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/>
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
68             public void UpdateMaxValue(float value) => maxValue += value;
```

<br/>

If the entity has not recently taken damage (defined by `vitalRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:72:17:17:`            if (!QueryColliderIsEnabled() &amp;&amp; QueryLastCollision() &lt; vitalRegenDelay) `"/>), `canRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:17:5:5:`        protected bool canRegen;`"/> is set to `true`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:77:5:5:`                canRegen = true;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
70             protected IEnumerator RegenDelay()
71             {
72                 if (!QueryColliderIsEnabled() && QueryLastCollision() < vitalRegenDelay) 
73                     yield return null;
74                 else
75                 {
76                     yield return new WaitForSeconds(vitalRegenDelay);
77                     canRegen = true;
78                 }
79             }
```

<br/>

A `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/> can call this on itself to register it to this controller, storing a reference to itself, as well as its enabled state.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
81             public void RegisterColliderToController(VitalsColliderController colliderController)
82             {
83                 if (colliderControllers.ContainsKey(colliderController)) return;
84                 
85                 colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
86             }
```

<br/>

Adjusts the enabled state of the given `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>, registering it if it was not so.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
88             public void SetColliderEnabled(VitalsColliderController colliderController)
89             {
90                 foreach (var key in colliderControllers.Keys)
91                 {
92                     if (key == colliderController)
93                     {
94                         colliderControllers[key] = colliderController.GetColliderEnabledState();
95                         return;
96                     }
97                 }
98                 
99                 colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
100                print($"added new VitalsColliderController {colliderController} with a value of {colliderControllers[colliderController]}");
101            }
```

<br/>

Returns true if any registered colliders are enabled.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
107            public bool QueryColliderIsEnabled() => colliderControllers.Values.Any(value => value);
```

<br/>

Returns the amount of time since a registered collider has detected a collision.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
113            public float QueryLastCollision()
114            {
115                var shortestTime = Mathf.Infinity;
116    
117                foreach (var key in colliderControllers.Keys)
118                {
119                    if (key.TimeSinceLastCollision < shortestTime)
120                        shortestTime = key.TimeSinceLastCollision;
121                }
122    
123                return shortestTime;
124            }
```

<br/>

A cluster of functionality that resets the timer for regeneration of this metric.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
126            protected void RestartRegenCountdown()
127            {
128                canRegen = false;
129                StopAllCoroutines();
130                StartCoroutine(RegenDelay());
131            }
```

<br/>

Forces every registered collider to cache its enabled state.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
133            protected void RefreshColliderEnabledStates()
134            {
135                var registeredColliders = colliderControllers.Keys;
136                colliderControllers.Clear();
137    
138                foreach (var colliderController in registeredColliders)
139                    colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
140            }
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/5g4tm).
