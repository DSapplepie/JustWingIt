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

	#endregion
	#region Variables: Glide
	[SerializeField] private float glideGravityMultiplier = 0.5f; // how much to scale gravity when gliding
	[SerializeField] private float maxGlideFallSpeed = -2f;       // the fastest downward velocity allowed while gliding
	private bool _isGliding;

	#endregion
	
	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		ApplyRotation();
		ApplyGravity();
		ApplyMovement();
	}

	private void ApplyGravity()
	{
    	// only stomp velocity when grounded and already moving down
    	if (IsGrounded() && _velocity < 0f)
    	{
        	_velocity = -1f;
        	_isGliding = false;
    	}
    	else
    	{
        	if (_isGliding && _velocity < 0f)
        	{
            	_velocity += _gravity * gravityMultiplier
                         	* glideGravityMultiplier
                         	* Time.deltaTime;
            	_velocity = Mathf.Max(_velocity, maxGlideFallSpeed);
        	}
        	else
        	{
            	_velocity += _gravity * gravityMultiplier * Time.deltaTime;
        	}
    	}

    	_direction.y = _velocity;
	}

	
	private void ApplyRotation()
	{
		if (_input.sqrMagnitude == 0) return;

		_direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
		var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void ApplyMovement()
	{
    	// Determines the target horizontal speed
    	var targetHorizontalSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
    	// Using MoveTowards, allows a smoother transition to the target horizontal speed.
    	movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetHorizontalSpeed, movement.acceleration * Time.deltaTime);

    	// Calculate horizontal movement components, scaled by currentSpeed
    	float moveX = _direction.x * movement.currentSpeed;
    	float moveZ = _direction.z * movement.currentSpeed;

    	// Vertical movement component is _direction.y (which is _velocity)
    	// It is not scaled by horizontal movement.currentSpeed
    	float moveY = _direction.y;

    	// Construct the final movement vector
    	Vector3 finalMoveVector = new Vector3(moveX, moveY, moveZ);

    	// Apply the movement using the correctly constructed finalMoveVector, scaled by Time.deltaTime
    	_characterController.Move(finalMoveVector * Time.deltaTime);
	}

	public void Glide(InputAction.CallbackContext ctx)
	{
    	if (ctx.started)      _isGliding = true;
    	else if (ctx.canceled) _isGliding = false;
	}

	public void Move(InputAction.CallbackContext context)
	{
		_input = context.ReadValue<Vector2>();
		_direction = new Vector3(_input.x, 0.0f, _input.y);
	}
	
	public void Jump(InputAction.CallbackContext context)
	{
		if (!context.started) return;
		if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
		if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());
		
		_numberOfJumps++;
		_velocity = jumpPower;
	}

	public void Sprint(InputAction.CallbackContext context)
	{
		movement.isSprinting = context.started || context.performed;
	}

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