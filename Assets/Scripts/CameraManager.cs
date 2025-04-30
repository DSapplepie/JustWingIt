using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
	#region Variables

	[SerializeField] private Transform target;
	private float _distanceToPlayer;
	private Vector2 _input;

	[SerializeField] private MouseSensitivity mouseSensitivity;
	[SerializeField] private CameraAngle cameraAngle;

	private CameraRotation _cameraRotation;

	#endregion

	// Called when the script instance is loaded and sets the initial distance between the camera and player.
	private void Awake() => _distanceToPlayer = Vector3.Distance(transform.position, target.position);

	// Called whenever a look input is received from the player, reading the 2D input vector (in this case the mouse delta)
	public void Look(InputAction.CallbackContext context)
	{
		_input = context.ReadValue<Vector2>();
	}

	// Proccesses the camera rotation based on input
	// 1st line being for left/right, 2nd for up/down, and the 3rd to prevent the camera from going to far.
	private void Update()
	{
		_cameraRotation.Yaw += _input.x * mouseSensitivity.horizontal * BoolToInt(mouseSensitivity.invertHorizontal) * Time.deltaTime;
		_cameraRotation.Pitch += _input.y * mouseSensitivity.vertical * BoolToInt(mouseSensitivity.invertVertical) * Time.deltaTime;
		_cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, cameraAngle.min, cameraAngle.max);
	}

	// Applies camera transformations by applying the calculated rotation and repositioning the camera behind the palyer by a set distance.
	private void LateUpdate()
	{
		transform.eulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);
		transform.position = target.position - transform.forward * _distanceToPlayer;
	}

	
	private static int BoolToInt(bool b) => b ? 1 : -1;
}

[Serializable]
public struct MouseSensitivity
{
	public float horizontal;
	public float vertical;
	public bool invertHorizontal;
	public bool invertVertical;
}

public struct CameraRotation
{
	public float Pitch;
	public float Yaw;
}

[Serializable]
public struct CameraAngle
{
	public float min;
	public float max;
}