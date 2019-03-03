﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
	[Range(0, 100f)]
	public float MouseSensitivity = 15f;
	public float SneakSpeed = 1.31f;
	public float WalkSpeed = 4.17f;
	public float SprintSpeed = 5.612f;
	public float JumpHeight = 1.25f;    // sometimes changes (potions, etc.)
	public GameObject Camera;
	public bool UseGravity = true;

	public delegate void OnGroundEventHandler(object sender, OnGroundEventArgs e);
	public event OnGroundEventHandler OnGroundChanged;
	private bool _wasOnGround = true;

	// Update is called once per frame
	void Update()
	{
		Pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
		Pitch = Mathf.Clamp(Pitch, CameraMinX, CameraMaxX);

		Yaw += Input.GetAxis("Mouse X") * MouseSensitivity;

		Camera.transform.localEulerAngles = new Vector3(Pitch, Yaw, 0);

		// jumping
		if (Input.GetKey(KeyCode.Space) && OnGround)
		{
			Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * JumpHeight), Rigidbody.velocity.y);
		}
	}

	private void FixedUpdate()
	{
		Vector3 inputVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;    // get raw input from user

		// adjust velocity based on walking or sprinting
		if (Input.GetKey(KeyCode.LeftShift))
			inputVelocity *= SneakSpeed;
		else if (Input.GetKey(KeyCode.LeftControl))
			inputVelocity *= SprintSpeed;
		else
			inputVelocity *= WalkSpeed;

		// add in falling velocity
		inputVelocity.y = Rigidbody.velocity.y;

		// since the camera is the only part of the player that turns, we need to rotate the velocity vectors to the camera's looking position
		// so the user moves in the direction they are looking
		Vector3 rotatedVelocity = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0) * inputVelocity;

		// check if player ground status changed
		if (OnGround != _wasOnGround)
		{
			_wasOnGround = OnGround;
			OnGroundChanged?.Invoke(this, new OnGroundEventArgs() { OnGround = OnGround });
		}

		Rigidbody.useGravity = UseGravity;
		Rigidbody.velocity = rotatedVelocity;
	}
}

public class OnGroundEventArgs : EventArgs
{
	public bool OnGround { get; set; }
}
