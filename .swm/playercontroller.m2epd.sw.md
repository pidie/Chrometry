---
id: m2epd
title: PlayerController
file_version: 1.1.1
app_version: 1.1.2
---

# Overview

This script controls player input in regards to the player character. Tasks such as movement, using the weapon and interacting with the environment are all centralized here, calling on outside methods to handle specific tasks.

# Fields and Properties

<br/>

<br/>

Serialized Fields - Movement
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
16     		[SerializeField] private float movementSpeed = 5f;
17     		[SerializeField] private float runSpeedMultiplier = 1.6f;
18     		[SerializeField] private float crouchSpeedMultiplier = 0.5f;
19     		[SerializeField] private float jumpHeight = 3f;
```

<br/>

`movementSpeed`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:16:9:9:`		[SerializeField] private float movementSpeed = 5f;`"/> - how quickly the player can move<br/>
`runSpeedMultiplier`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:17:9:9:`		[SerializeField] private float runSpeedMultiplier = 1.6f;`"/> - how much faster the player runs than walks<br/>
`crouchSpeedMultiplier`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:18:9:9:`		[SerializeField] private float crouchSpeedMultiplier = 0.5f;`"/> - how much faster the player moves while crouching than not

*   a value below 1 will move the player slower
    

`jumpHeight`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:19:9:9:`		[SerializeField] private float jumpHeight = 3f;`"/> - how high the player will jump

<br/>

<br/>

Serialized Fields - References
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
22     		[SerializeField] private GameObject cameraPosition;
23     		[SerializeField] private ItemData item;
24     		[SerializeField] private GameObject weaponWheelCanvas;
```

<br/>

`cameraPosition`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:22:9:9:`		[SerializeField] private GameObject cameraPosition;`"/> - used by a Cinemachine Virtual Cam to follow the player<br/>
`item`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:23:9:9:`		[SerializeField] private ItemData item;`"/> - the item that the player is holding<br/>
`weaponWheelCanvas`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:24:9:9:`		[SerializeField] private GameObject weaponWheelCanvas;`"/> - the canvas that contains the weapon wheel

<br/>

<br/>

Private Fields - Player Input
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
27     		private PlayerInputActions _playerControls;
28     		private InputAction _move;
29     		private InputAction _jump;
30     		private InputAction _run;
31     		private InputAction _crouch;
32     		private InputAction _interact;
33     		private InputAction _fire;
34     		private InputAction _secondary;
35     		private InputAction _useItem;
36     		private InputAction _weaponWheel;
```

<br/>

`_playerControls`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:27:5:5:`		private PlayerInputActions _playerControls;`"/> - a map of all the controls and the actions they're assigned to

*   Each `InputAction` controls a specific action. When the mapped button is pressed, that action is called, providing a callback context.
    
    *   The default context is `waiting`. The other three are `started`, `performed`, and `canceled`.
        
    *   These contexts are used to determine precisely how to execute our actions.

<br/>

<br/>

Private Fields - Unsorted
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
39     		private CharacterController _characterController;
40     		private Transform _cameraTransform;
41     		private Vector3 _movementDirection;
42     		private Vector3 _velocity;
43     		private Vector3 _cameraLocalPosition;
44     		private Vector3 _cameraLocalCrouchPosition;
45     		private bool _isRunning;
46     		private bool _isCrouching;
47     		private GameObject _weaponWheelInstance;
48     		private GravitationalForce _gravitationalForce;
49     		private Gun _gun;
```

<br/>

`_characterController`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:39:5:5:`		private CharacterController _characterController;`"/> - the character controller assigned to the player<br/>
`_cameraTransform`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:40:5:5:`		private Transform _cameraTransform;`"/> - a reference to the player camera (default - `Camera.main`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:63:17:19:`			if (Camera.main != null) _cameraTransform = Camera.main.transform;`"/>)<br/>
`_movementDirection`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:41:5:5:`		private Vector3 _movementDirection;`"/> - stores the delta information from player input as a Vector2<br/>
`_velocity`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:42:5:5:`		private Vector3 _velocity;`"/> - the speed (and direction) in which the player is moving<br/>
`_cameraLocalPosition`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:43:5:5:`		private Vector3 _cameraLocalPosition;`"/> - the base position of the camera<br/>
`_cameraLocalCrouchPosition`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:44:5:5:`		private Vector3 _cameraLocalCrouchPosition;`"/> - the base position of the camera while crouching

*   Once a player model with animations is implemented, this value will become obsolete.
    

`_isRunning`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:45:5:5:`		private bool _isRunning;`"/> - returns true if the player pressed the button to run<br/>
`_isCrouching`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:46:5:5:`		private bool _isCrouching;`"/> - returns true if the player pressed the button to crouch<br/>
`_weaponWheelInstance`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:47:5:5:`		private GameObject _weaponWheelInstance;`"/> - a reference to the active instance of the weapon wheel<br/>
`_gravitationalForce`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:48:5:5:`		private GravitationalForce _gravitationalForce;`"/> - a reference to the script controlling the artificial gravity<br/>
`_gun`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:49:5:5:`		private Gun _gun;`"/> - a reference to the player's gun

<br/>

<br/>

Public Properties
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
51     		public bool IsHoldingItem { get; set; }
52     		public bool IsInRangeOfInteractable { get; set; }
53     		public List<IInteractable> InteractablesInRange { get; set; } = new ();
```

<br/>

`IsHoldingItem`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:51:5:5:`		public bool IsHoldingItem { get; set; }`"/> - returns true if `item`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:23:9:9:`		[SerializeField] private ItemData item;`"/> is not `null`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:63:10:10:`			if (Camera.main != null) _cameraTransform = Camera.main.transform;`"/><br/>
`IsInRangeOfInteractable`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:52:5:5:`		public bool IsInRangeOfInteractable { get; set; }`"/> - returns true if the player is within interaction range of an interactable<br/>
`InteractablesInRange`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:53:8:8:`		public List&lt;IInteractable&gt; InteractablesInRange { get; set; } = new ();`"/> - a sorted list of interactables within range of the player

<br/>

<br/>

Public Actions / Public Static Actions
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
55     		public Action<ItemData> onAddItemToPlayer;
56     		public static Action<Sprite> onDisplayItemInUI;
```

<br/>

`onAddItemToPlayer`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:55:8:8:`		public Action&lt;ItemData&gt; onAddItemToPlayer;`"/> - called by an item controller to add the item to the player<br/>
`onDisplayItemInUI`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:56:10:10:`		public static Action&lt;Sprite&gt; onDisplayItemInUI;`"/> - called to push the item data to the UI

# Methods

<br/>

<br/>

Initializes private fields with necessary references
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
59     		private void Awake()
60     		{
61     			_playerControls = new PlayerInputActions();
62     			_characterController = GetComponent<CharacterController>();
63     			if (Camera.main != null) _cameraTransform = Camera.main.transform;
64     			_cameraLocalPosition = cameraPosition.transform.localPosition;
65     			_cameraLocalCrouchPosition = new Vector3(_cameraLocalPosition.x, _cameraLocalPosition.y - 0.4f,
66     				_cameraLocalPosition.z);
67     			_gravitationalForce = GetComponent<GravitationalForce>();
68     			_gun = GetComponentInChildren<Gun>();
69     		}
```

<br/>

<br/>

<br/>

Registers events and input action callback contexts to the appropriate methods and enables player controls
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
72     		private void OnEnable()
73     		{
74     			onAddItemToPlayer += AddItemToPlayer;
75     			
76     			_move = _playerControls.Player.Move;
77     			_move.Enable();
78     			_move.performed += ctx => _movementDirection = ctx.ReadValue<Vector2>();
79     			_move.canceled += ctx => _movementDirection = ctx.ReadValue<Vector2>();
80     			
81     			_jump = _playerControls.Player.Jump;
82     			_jump.Enable();
83     			_jump.started += OnJump;
84     
85     			_run = _playerControls.Player.Run;
86     			_run.Enable();
87     			_run.started += OnRun;
88     			_run.canceled += OnRun;
89     
90     			_crouch = _playerControls.Player.Crouch;
91     			_crouch.Enable();
92     			_crouch.started += OnCrouch;
93     			_crouch.canceled += OnCrouch;
94     
95     			_interact = _playerControls.Player.Interact;
96     			_interact.Enable();
97     			_interact.performed += OnInteract;
98     			_interact.canceled += OnInteract;
99     			
100    			_fire = _playerControls.Player.Fire;
101    			_fire.Enable();
102    			_fire.performed += OnFire;
103    			_fire.canceled += OnFire;
104    
105    			// all three states are here because certain gun mods will have different effects, requiring different state checks
106    			_secondary = _playerControls.Player.Secondary;
107    			_secondary.Enable();
108    			_secondary.started += OnSecondary;
109    			_secondary.performed += OnSecondary;
110    			_secondary.canceled += OnSecondary;
111    
112    			_useItem = _playerControls.Player.UseItem;
113    			_useItem.Enable();
114    			_useItem.performed += OnUseItem;
115    
116    			_weaponWheel = _playerControls.Player.WeaponWheel;
117    			_weaponWheel.Enable();
118    			_weaponWheel.performed += OnWeaponWheelCreate;
119    			_weaponWheel.canceled += OnWeaponWheelDestroy;
120    		}
```

<br/>

<br/>

<br/>

Deregisters events from their methods
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
123    		private void OnDisable()
124    		{
125    			onAddItemToPlayer -= AddItemToPlayer;
126    			
127    			_move.Disable();
128    			_fire.Disable();
129    			_jump.Disable();
130    			_run.Disable();
131    			_crouch.Disable();
132    			_secondary.Disable();
133    			_useItem.Disable();
134    			_weaponWheel.Disable();
135    		}
```

<br/>

<br/>

<br/>

Updates player position and `_velocity`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:42:5:5:`		private Vector3 _velocity;`"/>
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
138    		private void FixedUpdate()
139    		{
140    			var move = new Vector3(_movementDirection.x, 0f, _movementDirection.y);
141    			move =
142    				_cameraTransform.forward * move.z + _cameraTransform.right * move.x;
143    			move = new Vector3(move.x, 0, move.z);
144    
145    			var speed = movementSpeed;
146    
147    			if (_isCrouching)
148    			{
149    				speed *= crouchSpeedMultiplier;
150    				cameraPosition.transform.localPosition = _cameraLocalCrouchPosition;
151    			}
152    			else if (_isRunning)
153    			{
154    				speed *= runSpeedMultiplier;
155    				cameraPosition.transform.localPosition = _cameraLocalPosition;
156    			}
157    			else
158    			{
159    				cameraPosition.transform.localPosition = _cameraLocalPosition;
160    			}
161    			
162    			_characterController.Move(move * (speed * Time.deltaTime));
163    		}
```

<br/>

<br/>

<br/>

Adds upward force and cancels the `_isCrouching`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:171:1:1:`			_isCrouching = false;`"/> state when a player hits the jump button
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
169    		private void OnJump(InputAction.CallbackContext ctx)
170    		{
171    			_isCrouching = false;
172    			_gravitationalForce.Jump(jumpHeight);
173    		}
```

<br/>

<br/>

<br/>

Assigns the values of `_isRunning`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:175:16:16:`		private void OnRun(InputAction.CallbackContext ctx) =&gt; _isRunning = ctx.started;`"/> and `_isCrouching`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:177:16:16:`		private void OnCrouch(InputAction.CallbackContext ctx) =&gt; _isCrouching = ctx.started;`"/> based on player input
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
175    		private void OnRun(InputAction.CallbackContext ctx) => _isRunning = ctx.started;
176    
177    		private void OnCrouch(InputAction.CallbackContext ctx) => _isCrouching = ctx.started;
```

<br/>

<br/>

<br/>

Calls on the first item in the `InteractablesInRange`<swm-token data-swm-token=":Assets/Scripts/Player/PlayerController.cs:183:1:1:`			InteractablesInRange[0].Interact(this, ctx);`"/> list to be interacted with
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
179    		private void OnInteract(InputAction.CallbackContext ctx)
180    		{
181    			if (!IsInRangeOfInteractable) return;
182    
183    			InteractablesInRange[0].Interact(this, ctx);
184    		}
```

<br/>

<br/>

<br/>

Calls on the gun to fire or use the secondary function respectively
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
186    		private void OnFire(InputAction.CallbackContext ctx) => _gun.FireRequest(ctx);
187    
188    		private void OnSecondary(InputAction.CallbackContext ctx) => _gun.Secondary(ctx);
```

<br/>

<br/>

<br/>

Consumes the item being held
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
190    		private void OnUseItem(InputAction.CallbackContext ctx)
191    		{
192    			if (item == default) return;
193    			
194    			item.functions.Invoke();
195    			item = default;
196    			IsHoldingItem = false;
197    			onDisplayItemInUI.Invoke(null);
198    		}
```

<br/>

<br/>

<br/>

Creates an instance of the weapon wheel on the screen
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
200    		private void OnWeaponWheelCreate(InputAction.CallbackContext ctx)
201    		{
202    			if (_weaponWheelInstance == default)
203    			{
204    				UserInterfaceManager.onCreateCanvas.Invoke(weaponWheelCanvas);
205    				_weaponWheelInstance = UserInterfaceManager.LastCreatedCanvas;
206    			}
207    		}
```

<br/>

<br/>

<br/>

Destroys the weapon wheel
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
209    		private void OnWeaponWheelDestroy(InputAction.CallbackContext ctx) => Destroy(_weaponWheelInstance);
```

<br/>

<br/>

<br/>

Adds the item data to the player controller
<!-- NOTE-swimm-snippet: the lines below link your snippet to Swimm -->
### 📄 Assets/Scripts/Player/PlayerController.cs
```c#
215    		private void AddItemToPlayer(ItemData i)
216    		{
217    			item = i;
218    			IsHoldingItem = true;
219    			onDisplayItemInUI.Invoke(i.icon);
220    			print($"Player received {i.itemName} item");
221    		}
```

<br/>

<br/>

<br/>

This file was generated by Swimm. [Click here to view it in the app](https://app.swimm.io/repos/Z2l0aHViJTNBJTNBQ2hyb21ldHJ5JTNBJTNBcGlkaWU=/docs/m2epd).
