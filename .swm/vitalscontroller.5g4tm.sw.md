---
id: 5g4tm
title: VitalsController
file_version: 1.1.1
app_version: 1.0.20
---

# Overview

The `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> is an abstract class for tracking a metric, with a subclass for each metric. All actors and structures that make use of vitals (collectively called _entities_) will have a component that inherits from this class.

<br/>

## Purpose

The `VitalsController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:9:7:7:`    public abstract class VitalsController : MonoBehaviour`"/> grants essential functionality to a multitude of subclasses and dependent classes. It acts as an information hub, tracking and adjusting its values based on incoming information.

<br/>

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
18             
19             // todo : convert these lists to a dictionary
20             protected List<VitalsColliderController> colliderControllers = new ();
21             protected List<bool> colliderEnabledStates = new ();
```

<br/>

`currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> - the current value of the metric<br/>
`canRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:17:5:5:`        protected bool canRegen;`"/> - keeps track of whether or not this metric can regenerate<br/>
`colliderControllers`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:20:8:8:`        protected List&lt;VitalsColliderController&gt; colliderControllers = new ();`"/> - a list of colliders that register to this controller<br/>
`colliderEnabledStates`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:21:8:8:`        protected List&lt;bool&gt; colliderEnabledStates = new ();`"/> - a list of Boolean values that reflect the enabled state of the colliders in `colliderControllers`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:20:8:8:`        protected List&lt;VitalsColliderController&gt; colliderControllers = new ();`"/>

*   _`colliderControllers`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:20:8:8:`        protected List&lt;VitalsColliderController&gt; colliderControllers = new ();`"/> and `colliderEnabledStates`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:21:8:8:`        protected List&lt;bool&gt; colliderEnabledStates = new ();`"/> will be combined into a Dictionary<_`VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>_, bool>_
    

<br/>

<br/>

Actions
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
23             public Action onUpdateDisplay;
24             public Action<bool> onToggleCollider;
```

<br/>

`onUpdateDisplay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:23:5:5:`        public Action onUpdateDisplay;`"/> - invoked when the UI display needs to be updated<br/>
`onToggleCollider`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:24:8:8:`        public Action&lt;bool&gt; onToggleCollider;`"/> - invoked when a registered collider should be toggled on or off - passing `true` enabled the collider

<br/>

<br/>

Public Accessors
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
26             public float CurrentValue => currentValue;
27             public float MaxValue => maxValue;
```

<br/>

<br/>

# Methods

Below, find each method and a brief explanation of what it does.

<br/>

Sets the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> to whatever the `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:29:9:9:`        public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)`"/> is and adjusts the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> as needed.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
29             public void SetMaxValue(float value, bool maxOutCurrentValue = true, bool increaseCurrentValue = false)
30             {
31                 maxValue = value;
32     
33                 if (maxOutCurrentValue)
34                     currentValue = maxValue;
35                 else if (increaseCurrentValue)
36                     UpdateValue(value);
37             }
```

<br/>

Sets the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/> and calls to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
39             protected virtual void Awake()
40             {
41                 currentValue = currentValueOverride < maxValue ? maxValue : currentValueOverride;
42                 onUpdateDisplay?.Invoke();
43             }
```

<br/>

Regenerates the metric and ensures it doesn't exceed the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/> and calls to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
45             protected void Update()
46             {
47                 if (currentValue > maxValue) currentValue = maxValue;
48                 if (currentValue < maxValue && canRegen)
49                 {
50                     currentValue += vitalRegen * Time.deltaTime;
51                     onUpdateDisplay?.Invoke();
52                 }
53             }
```

<br/>

Adds the `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:55:11:11:`        public virtual void UpdateValue(float value)`"/> to the `currentValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:16:5:5:`        protected float currentValue;`"/>, stopping regeneration if `value`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:55:11:11:`        public virtual void UpdateValue(float value)`"/> is negative and calling to update the UI display.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
55             public virtual void UpdateValue(float value)
56             {            
57                 currentValue += value;
58                 if (value < 0)
59                 {
60                     canRegen = false;
61                     StopAllCoroutines();
62                     StartCoroutine(RegenDelay());
63     
64                     RefreshColliderEnabledStates();
65                 }
66     
67                 onUpdateDisplay?.Invoke();
68             }
```

<br/>

Updates the `maxValue`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:11:9:9:`        [SerializeField] protected float maxValue;`"/>
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
70             public void UpdateMaxValue(float value) => maxValue += value;
```

<br/>

If the entity has not recently taken damage (defined by `vitalRegenDelay`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:74:17:17:`            if (!QueryColliderIsEnabled() &amp;&amp; QueryLastCollision() &lt; vitalRegenDelay) `"/>), `canRegen`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:17:5:5:`        protected bool canRegen;`"/> is set to `true`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsController.cs:79:5:5:`                canRegen = true;`"/>.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
72             protected IEnumerator RegenDelay()
73             {
74                 if (!QueryColliderIsEnabled() && QueryLastCollision() < vitalRegenDelay) 
75                     yield return null;
76                 else
77                 {
78                     yield return new WaitForSeconds(vitalRegenDelay);
79                     canRegen = true;
80                 }
81             }
```

<br/>

A `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/> can call this on itself to register it to this controller, storing a reference to itself, as well as its enabled state.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
83             public void RegisterColliderToController(VitalsColliderController colliderController)
84             {
85                 if (colliderControllers.Contains(colliderController)) return;
86                 
87                 colliderControllers.Add(colliderController);
88                 colliderEnabledStates.Add(colliderController.GetColliderEnabledState());
89             }
```

<br/>

Adjusts the enabled state of the given `VitalsColliderController`<swm-token data-swm-token=":Assets/Scripts/Vitals/VitalsColliderController.cs:6:5:5:`    public class VitalsColliderController : MonoBehaviour`"/>, registering it if it was not so.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
91             public void SetColliderEnabled(VitalsColliderController colliderController)
92             {
93                 for (var i = 0; i < colliderControllers.Count; i++)
94                 {
95                     if (colliderControllers[i] == colliderController)
96                     {
97                         colliderEnabledStates[i] = colliderController.GetColliderEnabledState();
98                         return;
99                     }
100                }
101                
102                colliderControllers.Add(colliderController);
103                colliderEnabledStates.Add(colliderController.GetColliderEnabledState());
104                print($"added new VitalsColliderController {colliderController} with a value of {colliderEnabledStates[^1]}");
105            }
```

<br/>

Returns true if any registered colliders are enabled.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
111            public bool QueryColliderIsEnabled() => colliderEnabledStates.Any(state => state);
```

<br/>

Returns the amount of time since a registered collider has detected a collision.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
117            public float QueryLastCollision()
118            {
119                var shortestTime = Mathf.Infinity;
120                VitalsColliderController colliderController = default;
121                
122                foreach (var t in colliderControllers)
123                {
124                    if (t.TimeSinceLastCollision < shortestTime)
125                    {
126                        shortestTime = t.TimeSinceLastCollision;
127                        colliderController = t;
128                    }
129                }
130    
131                print($"{shortestTime}: {colliderController.gameObject.name}");
132                return shortestTime;
133            }
```

<br/>

A cluster of functionality that resets the timer for regeneration of this metric.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
135            protected void RestartRegenCountdown()
136            {
137                canRegen = false;
138                StopAllCoroutines();
139                StartCoroutine(RegenDelay());
140            }
```

<br/>

Forces every registered collider to cache its enabled state.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Vitals/VitalsController.cs
```c#
142            protected void RefreshColliderEnabledStates()
143            {
144                for (var i = 0; i < colliderControllers.Count; i++)
145                    colliderEnabledStates[i] = colliderControllers[i].GetColliderEnabledState();
146            }
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/5g4tm).
