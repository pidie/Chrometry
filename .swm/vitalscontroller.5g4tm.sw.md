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
12             [SerializeField] protected float currentValueOverride = -1f;
13             [SerializeField] protected float vitalRegen;
14             [SerializeField] protected float vitalRegenDelay;
```

<br/>

`maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> - the maximum value this metric can reach<br/>
`currentValueOverride`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:12:9:9:`        [SerializeField] protected float currentValueOverride = -1f;`"/> - starts the game with this metric at the given value<br/>
`vitalRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:13:9:9:`        [SerializeField] protected float vitalRegen;`"/> - how much of this metric you regain every second while regenerating<br/>
`vitalRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:14:9:9:`        [SerializeField] protected float vitalRegenDelay;`"/> - how many seconds after taking damage until regeneration can begin

<br/>

Protected Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
16             protected float currentValue;
17             protected bool canRegen;
18             protected Dictionary<VitalsColliderController, bool> colliderControllers = new ();
```

<br/>

`currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> - the current value of the metric<br/>
`canRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:17:5:5:`        protected bool canRegen;`"/> - keeps track of whether or not this metric can regenerate<br/>
`colliderControllers`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:18:11:11:`        protected Dictionary&lt;VitalsColliderController, bool&gt; colliderControllers = new ();`"/> - a collection of collider controllers that register to this controller and the enabled state of the collider components

<br/>

Actions
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
20             public Action onUpdateDisplay;
21             public Action<bool> onToggleCollider;
```

<br/>

`onUpdateDisplay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:20:5:5:`        public Action onUpdateDisplay;`"/> - invoked when the UI display needs to be updated<br/>
`onToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:21:8:8:`        public Action&lt;bool&gt; onToggleCollider;`"/> - invoked when a registered collider should be toggled on or off - passing `true` enabled the collider

<br/>

Public Accessers
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
23             public float CurrentValue => currentValue;
24             public float MaxValue => maxValue;
```

<br/>

<br/>

# Methods

Below, find each method and a brief explanation of what it does.

<br/>

Sets the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> to whatever the `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:26:9:9:`        public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)`"/> is and adjusts the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> as needed.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
26             public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)
27             {
28                 maxValue = value;
29     
30                 if (maxOutCurrentValue)
31                     currentValue = maxValue;
32                 else if (increaseCurrentValue)
33                     UpdateValue(value);
34             }
```

<br/>

Sets the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> and calls to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
36             protected virtual void Awake()
37             {
38                 currentValue = currentValueOverride < maxValue && currentValueOverride >= 0
39                     ? currentValueOverride : maxValue;
40                 UpdateValue(0);
41             }
```

<br/>

Regenerates the metric and ensures it doesn't exceed the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> and calls to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
43             protected virtual void Update()
44             {
45                 if (currentValue > maxValue) currentValue = maxValue;
46                 if (currentValue < maxValue && canRegen)
47                 {
48                     currentValue += vitalRegen * Time.deltaTime;
49                     onToggleCollider?.Invoke(true);
50                     onUpdateDisplay?.Invoke();
51                 }
52             }
```

<br/>

Adds the `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:54:11:11:`        public virtual void UpdateValue(float value)`"/> to the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/>, stopping regeneration if `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:54:11:11:`        public virtual void UpdateValue(float value)`"/> is negative and calling to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
54             public virtual void UpdateValue(float value)
55             {
56                 if (!QueryColliderIsEnabled()) return;
57                 
58                 currentValue += value;
59                 if (value < 0)
60                 {
61                     RestartRegenCountdown();
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

If the entity has not recently taken damage (defined by `vitalRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:72:17:17:`            if (!QueryColliderIsEnabled() &amp;&amp; QueryLastCollision() &lt; vitalRegenDelay) `"/>), `canRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:17:5:5:`        protected bool canRegen;`"/> is set to `true`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:78:5:5:`                canRegen = true;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
70             protected virtual IEnumerator RegenDelay()
71             {
72                 if (!QueryColliderIsEnabled() && QueryLastCollision() < vitalRegenDelay) 
73                     yield return null;
74                 else
75                 {
76                     yield return new WaitForSeconds(vitalRegenDelay);
77     
78                     canRegen = true;
79                 }
80             }
```

<br/>

A `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:7:5:5:`    public class VitalsColliderController : MonoBehaviour`"/> can call this on itself to register it to this controller, storing a reference to itself, as well as its enabled state.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
82             public void RegisterColliderToController(VitalsColliderController colliderController)
83             {
84                 if (colliderControllers.ContainsKey(colliderController)) return;
85                 
86                 colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
87             }
```

<br/>

Adjusts the enabled state of the given `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:7:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>, registering it if it was not so.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
89             public void SetColliderEnabled(VitalsColliderController colliderController)
90             {
91                 foreach (var key in colliderControllers.Keys)
92                 {
93                     if (key == colliderController)
94                     {
95                         colliderControllers[key] = colliderController.GetColliderEnabledState();
96                         return;
97                     }
98                 }
99                 
100                colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
101                print($"added new VitalsColliderController {colliderController} with a value of {colliderControllers[colliderController]}");
102            }
```

<br/>

Returns true if any registered colliders are enabled.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
108            public bool QueryColliderIsEnabled() => colliderControllers.Values.Any(value => value);
```

<br/>

Returns the amount of time since a registered collider has detected a collision.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
114            public float QueryLastCollision()
115            {
116                var shortestTime = Mathf.Infinity;
117    
118                foreach (var key in colliderControllers.Keys)
119                {
120                    if (key.TimeSinceLastCollision < shortestTime)
121                        shortestTime = key.TimeSinceLastCollision;
122                }
123    
124                return shortestTime;
125            }
```

<br/>

A cluster of functionality that resets the timer for regeneration of this metric.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
127            protected void RestartRegenCountdown()
128            {
129                canRegen = false;
130                StopAllCoroutines();
131                StartCoroutine(RegenDelay());
132            }
```

<br/>

Forces every registered collider to cache its enabled state.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
134            protected void RefreshColliderEnabledStates()
135            {
136                var registeredColliders = colliderControllers.Keys;
137                colliderControllers.Clear();
138    
139                foreach (var colliderController in registeredColliders)
140                    colliderControllers.Add(colliderController, colliderController.GetColliderEnabledState());
141            }
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/5g4tm).
