using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public string Player = "";
	public float MovementSpeed = 5f;
	public float RotationSpeed = 800f;
	public bool Locked;

	Transform myTransform;
	Rigidbody myRigidbody;
	Transform cameraHolder;


	// Start is called before the first frame update
	void Start()
	{
		myTransform = this.transform;
		myRigidbody = GetComponent<Rigidbody>();
		cameraHolder = transform.Find("CameraHolder");
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		if (Locked) return;

		// get inputs
		var inputX = Input.GetAxis("Horizontal" + Player);
		var inputY = Input.GetAxis("Vertical" + Player);
		//var inputR = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f);

		// get current position and rotation, then do calculations
		// position
		var moveVectorX = Vector3.forward * inputY;
		var moveVectorY = Vector3.right * inputX;
		var targetVector = moveVectorX + moveVectorY;
		var moveVector = targetVector.normalized;

		// rotation
		//currentRotation = ClampAngle(currentRotation + (inputR * rotationSpeed));
		if (moveVector.sqrMagnitude > 0)
		{
			var rotationAngle = Quaternion.LookRotation(moveVector, Vector3.up);
			// update Character position and rotation
			myRigidbody.MoveRotation(Quaternion.RotateTowards(myTransform.rotation, rotationAngle, RotationSpeed * Time.fixedDeltaTime));
			//myRigidbody.MovePosition(myTransform.position + myTransform.forward * MovementSpeed * Time.fixedDeltaTime);
		}

		moveVector *= 7;
		myRigidbody.velocity = (new Vector3(moveVector.x, myRigidbody.velocity.y, moveVector.z) + myRigidbody.velocity) / 2;
	}
}
