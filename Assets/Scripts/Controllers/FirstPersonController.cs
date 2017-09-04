using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {

	CharacterController characterController;

	public float moveSpeed = 5f;
	public float mouseSpeed = 1.5f;
	public float jumpSpeed = 10f;

	float verticalRotation = 0f;
	public float pitchRange = 60.0f;

	float verticalVelocity = 0f;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		// Rotation
		float yaw = Input.GetAxis("Mouse X") * mouseSpeed;
		transform.Rotate (0, yaw, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSpeed;
		verticalRotation = Mathf.Clamp (verticalRotation, -pitchRange, pitchRange);

		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement
		float forwardSpeed = Input.GetAxis ("Vertical") * moveSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * moveSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if (characterController.isGrounded) {
			if (verticalVelocity < 0) {
				verticalVelocity = 0;
			}
			if (Input.GetButton ("Jump")) {
				verticalVelocity = jumpSpeed;
			}
		}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;

		characterController.Move(speed * Time.deltaTime);

		// Cursor lockstate
		if (Input.GetButtonDown ("Cancel")) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			}
			else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}
}
