using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	float movementSpeed = 5.0f;
	float rotationSpeed = 5.0f;

	Transform myTransform;
	Transform cameraHolder;

	// Start is called before the first frame update
	void Start()
	{
		myTransform = this.transform;
		cameraHolder = transform.Find("CameraHolder");
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		// get inputs
		var inputX = Input.GetAxis("Horizontal");
		var inputY = Input.GetAxis("Vertical");
		//var inputR = Mathf.Clamp(Input.GetAxis("Mouse X"), -1.0f, 1.0f);

		// get current position and rotation, then do calculations
		// position
		var moveVectorX = Vector3.forward * inputY;
		var moveVectorY = Vector3.right * inputX;
		var targetVector = moveVectorX + moveVectorY;
		var moveVector = targetVector.normalized;

		// rotation
		//currentRotation = ClampAngle(currentRotation + (inputR * rotationSpeed));
		if (targetVector.sqrMagnitude > 0)
		{
			var rotationAngle = Quaternion.LookRotation(moveVector, Vector3.up);
			// update Character position and rotation
			myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, rotationAngle, 500f * Time.deltaTime);
			myTransform.position = myTransform.position + myTransform.forward * movementSpeed * Time.deltaTime;
		}

	}

	float ClampAngle(float theAngle)
	{
		if (theAngle < -360.0f)
		{
			theAngle += 360.0f;
		}
		else if (theAngle > 360.0f)
		{
			theAngle -= 360.0f;
		}

		return theAngle;
	}
}
