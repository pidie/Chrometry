---
id: s4mjd
title: GravitationalForce
file_version: 1.1.1
app_version: 1.1.4
---

# Overview

This script is responsible for supplying controlled, artificial gravity to game objects it is attached to.

Each physics update, a downward force is added to either the transform or the `CharacterController`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:15:3:3:`        private CharacterController _characterController;`"/> component if it is attached to a player character. This increase is stopped and reset when the object is grounded.

<br/>

## The Math

The formula for calculating the pull of gravity is:

> _9.81 m/s^2_

<br/>

To simulate this, we have to multiply the gravity constant of `-9.81f`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:17:11:14:`        private const float Gravity = -9.81f;`"/> by `Time.deltaTime`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:31:11:13:`            _velocity.y += Gravity * Time.deltaTime * gravityMultiplier;`"/> by `Time.deltaTime`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:31:11:13:`            _velocity.y += Gravity * Time.deltaTime * gravityMultiplier;`"/> again. We then multiply this by the `gravityMultiplier`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:12:9:9:`        [SerializeField] private float gravityMultiplier = 1f;`"/> to simulate low or high gravity situations or effects.

*   By doing this on each item, we have situations where certain objects are more affected by gravity than others.
    

# Fields and Properties

<br/>

Serialized Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
11             [SerializeField] private GroundCheck groundCheck;
12             [SerializeField] private float gravityMultiplier = 1f;
```

<br/>

`groundCheck`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:11:9:9:`        [SerializeField] private GroundCheck groundCheck;`"/> - used to check if the game object is grounded<br/>
`gravityMultiplier`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:12:9:9:`        [SerializeField] private float gravityMultiplier = 1f;`"/> - a multiplier to strengthen or weaken the effects of gravity

<br/>

Private Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
14             private Vector3 _velocity;
15             private CharacterController _characterController;
```

<br/>

`_velocity`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:14:5:5:`        private Vector3 _velocity;`"/> - the current speed caused by gravity<br/>
`_characterController`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:15:5:5:`        private CharacterController _characterController;`"/> - a reference to an attached character controller

*   Only used on a player character

<br/>

Private Constant Fields
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
17             private const float Gravity = -9.81f;
```

<br/>

`Gravity`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:17:7:7:`        private const float Gravity = -9.81f;`"/> - the amount of force added when an object is not grounded

<br/>

# Methods

<br/>

Calls the `groundCheck`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:19:11:11:`        public GroundCheck GetGroundCheck() =&gt; groundCheck;`"/> to see if the game object is grounded.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
19             public GroundCheck GetGroundCheck() => groundCheck;
```

<br/>

<br/>

<br/>

Grabs a reference to the `CharacterController`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:21:17:17:`        private void Awake() =&gt; _characterController = GetComponent&lt;CharacterController&gt;();`"/> if there is one.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
21             private void Awake() => _characterController = GetComponent<CharacterController>();
```

<br/>

<br/>

<br/>

If the game object is grounded, sets the `_velocity`<swm-token data-swm-token=":Assets/Scripts/Tools/GravitationalForce.cs:25:12:12:`            if (groundCheck.CheckIsGrounded() &amp;&amp; _velocity.y &lt;= 0)`"/> to `0`; otherwise, adds downwards force to the game object.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
23             private void FixedUpdate()
24             {
25                 if (groundCheck.CheckIsGrounded() && _velocity.y <= 0)
26                 {
27                     _velocity.y = 0f;
28                     return;
29                 }
30     
31                 _velocity.y += Gravity * Time.deltaTime * gravityMultiplier;
32     
33                 if (_characterController)
34                     _characterController.Move(_velocity * Time.deltaTime);
35                 else
36                     transform.position += _velocity * Time.deltaTime;
37             }
```

<br/>

<br/>

<br/>

Adds a quick burst of force to the game object's `up` vector.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
39             public void Jump(float jumpHeight)
40             {
41                 if (groundCheck.CheckIsGrounded())
42                     _velocity.y = Mathf.Sqrt(jumpHeight * -2 * Gravity);
43             }
```

<br/>

<br/>

<br/>

Adds force in the given direction, similar to a jump.
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Tools/GravitationalForce.cs
```c#
46             public void Throw(Vector3 direction, float force)
47             {
48                 var directionX = Mathf.Sqrt(direction.x * -2 * Gravity);
49                 var directionY = Mathf.Sqrt(direction.y * -2 * Gravity);
50                 var directionZ = Mathf.Sqrt(direction.z * -2 * Gravity);
51     
52                 _velocity = new Vector3(directionX, directionY, directionZ).normalized * force;
53             }
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/s4mjd).
