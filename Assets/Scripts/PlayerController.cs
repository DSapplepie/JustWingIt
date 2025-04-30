using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	#region Variables: Movement

	private Vector2 _input;
	private CharacterController _characterController;
	private Vector3 _direction;

	[SerializeField] private float speed;

	[SerializeField] private Movement movement;

	#endregion
	#region Variables: Rotation

	[SerializeField] private float rotationSpeed = 500f;
	private Camera _mainCamera;

	#endregion
	#region Variables: Gravity

	private float _gravity = -9.81f;
	[SerializeField] private float gravityMultiplier = 3.0f;
	private float _velocity;

	#endregion
	#region Variables: Jumping

	[SerializeField] private float jumpPower;
	private int _numberOfJumps;
	[SerializeField] private int maxNumberOfJumps = 2;

	[SerializeField] private float glideFallSpeed = -2f;
	private bool _wantsToGlide;

	#endregion
	
	// Gets references on script startup to both the main camera and the CharacterController component attached to the player gameobject.
	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		_mainCamera = Camera.main;
	}

	// Updates different aspects for the player based on the inputs by calling each functions every frame.
	private void Update()
	{
		ApplyRotation();
		ApplyGravity();
		ApplyMovement();
	}

	
	private void ApplyGravity()
	{
		// Resets downward velocity if its grounded and the velocity is less than 0
		if (IsGrounded() && _velocity < 0.0f)
		{
			_velocity = -1.0f;
		}
		// If gliding, then reduce fall speed slowly
		else if(_wantsToGlide)
		{
			_velocity += glideFallSpeed * Time.deltaTime;
		}
		// Else, apply normal gravity * mult
		else
		{
			_velocity += _gravity * gravityMultiplier * Time.deltaTime;
		}
		
		_direction.y = _velocity;
	}
	
	private void ApplyRotation()
	{
		// If there's no input then ignore rest of code
		if (_input.sqrMagnitude == 0) return;

		// Converts the input direction in relation to the camera angle
		_direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
		// Finds desired rotation given direction
		var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
		// Rotate the player towards the target rotation
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void ApplyMovement()
	{
		// Determines the targets speed
		var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
		// Using MoveTowards, allows a smoother transition to the target speed.
		movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);
		// Move the character using the controller
		_characterController.Move(_direction * movement.currentSpeed * Time.deltaTime);
	}

	public void Move(InputAction.CallbackContext context)
	{
		// Called by the input system to update the movement input
		_input = context.ReadValue<Vector2>();
		// Updates horizontal direction
		_direction = new Vector3(_input.x, 0.0f, _input.y);
	}

	public void Jump(InputAction.CallbackContext context)
	{
		// Triggers the hump on input start
		if (!context.started) return;
		// If the character is in midair and is out of jumps upon input press, then glide.
		if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps)
		{
			Debug.Log("Glide");
			_wantsToGlide = true;
			return;
		} 
		// Otherwise, it either starts the coroutine to reset the jump count whenever the player lands on the ground
		// or it keeps track of the jumps, applies the jumpForce, and cancels the gliding.
		else 
		{ 
			if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());
		
			_numberOfJumps++;
			_velocity = jumpPower;
			_wantsToGlide = false;
		}
	}

	// Sets the sprinting flag based off the input (holding down shift or not)
	public void Sprint(InputAction.CallbackContext context)
	{
		movement.isSprinting = context.started || context.performed;
	}

	// Waits for the player to leave the ground, hit the ground, and resets the jump counter after both events.
	private IEnumerator WaitForLanding()
	{
		yield return new WaitUntil(() => !IsGrounded());
		yield return new WaitUntil(IsGrounded);

		_numberOfJumps = 0;
	}

	private bool IsGrounded() => _characterController.isGrounded;
}

[Serializable]
public struct Movement
{
	public float speed;
	public float multiplier;
	public float acceleration;

	[HideInInspector] public bool isSprinting;
	[HideInInspector] public float currentSpeed;
}