using System;
using Data.Scripts;
using Interfaces;
using Managers;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField] private float movementSpeed = 5f;
		[SerializeField] private float runSpeedMultiplier = 1.6f;
		[SerializeField] private float crouchSpeedMultiplier = 0.5f;
		[SerializeField] private float jumpHeight = 3f;

		[Header("User Input")] 
		[SerializeField] private float mouseSensitivity = 0.8f;
		
		[Header("References")]
		[SerializeField] private GameObject cameraPosition;
		[SerializeField] private ItemData item;
		[SerializeField] private GameObject weaponWheelCanvas;
		
		// input actions
		private PlayerInputActions _playerControls;
		private InputAction _move;
		private InputAction _jump;
		private InputAction _run;
		private InputAction _crouch;
		private InputAction _interact;
		private InputAction _fire;
		private InputAction _secondary;
		private InputAction _useItem;
		private InputAction _weaponWheel;

		// other private variables
		private CharacterController _characterController;
		private Transform _cameraTransform;
		private Vector3 _movementDirection;
		private Vector3 _velocity;
		private Vector3 _cameraLocalPosition;
		private Vector3 _cameraLocalCrouchPosition;
		private bool _isRunning;
		private bool _isCrouching;
		private GameObject _weaponWheelInstance;
		private GravitationalForce _gravitationalForce;
		private Gun _gun;
		private CameraMouseSensitivityController _cameraMouseSensitivityController;

		public bool isHoldingItem;
		public bool IsInRangeOfInteractable { get; set; }
		public IInteractable Interactable { get; set; }

		public static Action<ItemData> onAddItemToPlayer;
		
		// initialization
		private void Awake()
		{
			_playerControls = new PlayerInputActions();
			_characterController = GetComponent<CharacterController>();
			if (Camera.main != null) _cameraTransform = Camera.main.transform;
			_cameraLocalPosition = cameraPosition.transform.localPosition;
			_cameraLocalCrouchPosition = new Vector3(_cameraLocalPosition.x, _cameraLocalPosition.y - 0.4f,
				_cameraLocalPosition.z);
			_gravitationalForce = GetComponent<GravitationalForce>();
			_gun = GetComponentInChildren<Gun>();
			_cameraMouseSensitivityController = GetComponent<CameraMouseSensitivityController>();
			_cameraMouseSensitivityController.UpdateMouseSensitivity(mouseSensitivity);
		}

		// subscribe to all events and input actions
		private void OnEnable()
		{
			onAddItemToPlayer += AddItemToPlayer;
			
			_move = _playerControls.Player.Move;
			_move.Enable();
			_move.performed += ctx => _movementDirection = ctx.ReadValue<Vector2>();
			_move.canceled += ctx => _movementDirection = ctx.ReadValue<Vector2>();
			
			_jump = _playerControls.Player.Jump;
			_jump.Enable();
			_jump.started += OnJump;

			_run = _playerControls.Player.Run;
			_run.Enable();
			_run.started += OnRun;
			_run.canceled += OnRun;

			_crouch = _playerControls.Player.Crouch;
			_crouch.Enable();
			_crouch.started += OnCrouch;
			_crouch.canceled += OnCrouch;

			_interact = _playerControls.Player.Interact;
			_interact.Enable();
			_interact.performed += OnInteract;
			
			_fire = _playerControls.Player.Fire;
			_fire.Enable();
			_fire.performed += OnFire;

			// all three states are here because certain gun mods will have different effects, requiring different state checks
			_secondary = _playerControls.Player.Secondary;
			_secondary.Enable();
			_secondary.started += OnSecondary;
			_secondary.performed += OnSecondary;
			_secondary.canceled += OnSecondary;

			_useItem = _playerControls.Player.UseItem;
			_useItem.Enable();
			_useItem.performed += OnUseItem;

			_weaponWheel = _playerControls.Player.WeaponWheel;
			_weaponWheel.Enable();
			_weaponWheel.performed += OnWeaponWheelCreate;
			_weaponWheel.canceled += OnWeaponWheelDestroy;
		}

		// unsubscribe from all events and input actions
		private void OnDisable()
		{
			onAddItemToPlayer -= AddItemToPlayer;
			
			_move.Disable();
			_fire.Disable();
			_jump.Disable();
			_run.Disable();
			_crouch.Disable();
			_secondary.Disable();
			_useItem.Disable();
			_weaponWheel.Disable();
		}

		// control player movement
		private void FixedUpdate()
		{
			var move = new Vector3(_movementDirection.x, 0f, _movementDirection.y);
			move =
				_cameraTransform.forward * move.z + _cameraTransform.right * move.x;
			move = new Vector3(move.x, 0, move.z);

			var speed = movementSpeed;

			if (_isCrouching)
			{
				speed *= crouchSpeedMultiplier;
				cameraPosition.transform.localPosition = _cameraLocalCrouchPosition;
			}
			else if (_isRunning)
			{
				speed *= runSpeedMultiplier;
				cameraPosition.transform.localPosition = _cameraLocalPosition;
			}
			else
			{
				cameraPosition.transform.localPosition = _cameraLocalPosition;
			}
			
			_characterController.Move(move * (speed * Time.deltaTime));
		}

		/*
		 * The following methods handle input via the new input system
		 */
		
		private void OnJump(InputAction.CallbackContext ctx)
		{
			_isCrouching = false;
			_gravitationalForce.Jump(jumpHeight);
		}

		private void OnRun(InputAction.CallbackContext ctx) => _isRunning = ctx.started;

		private void OnCrouch(InputAction.CallbackContext ctx) => _isCrouching = ctx.started;

		private void OnInteract(InputAction.CallbackContext ctx)
		{
			if (!IsInRangeOfInteractable) return;
			else
			{
				Interactable.Interact();
			}
		}

		private void OnFire(InputAction.CallbackContext ctx) => _gun.Fire();

		private void OnSecondary(InputAction.CallbackContext ctx)
		{
			if		(ctx.started)	print($"secondary started");
			else if (ctx.performed) print($"secondary performed");
			else if (ctx.canceled)	print($"secondary canceled");
		}

		private void OnUseItem(InputAction.CallbackContext ctx)
		{
			if (item == default) return;
			
			item.functions.Invoke();
			item = default;
			isHoldingItem = false;
		}

		private void OnWeaponWheelCreate(InputAction.CallbackContext ctx)
		{
			if (_weaponWheelInstance == default)
			{
				UserInterfaceManager.onCreateCanvas.Invoke(weaponWheelCanvas);
				_weaponWheelInstance = UserInterfaceManager.LastCreatedCanvas;
			}
		}

		private void OnWeaponWheelDestroy(InputAction.CallbackContext ctx) => Destroy(_weaponWheelInstance);

		/*
		 * The following methods are internal methods that are required for the player controller
		 */
		
		private void AddItemToPlayer(ItemData i)
		{
			item = i;
			isHoldingItem = true;
			print($"Player received {i.itemName} item");
		}
	}
}