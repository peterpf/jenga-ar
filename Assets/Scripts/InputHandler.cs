using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

	RaycastHit hit;
	Ray ray;

	Vector3 gravity;
	Vector3 phoneMovement;

	GameObject selectedBlock;

	// Use this for initialization
	void Start()
	{
		Input.gyro.enabled = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (Input.touchCount == 1)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					checkTouchOnBlockObject(Input.GetTouch(0).position);
				} else
				{
					selectedBlock = null;
				}
			}
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			if (Input.GetMouseButtonDown(0))
			{
				checkTouchOnBlockObject(Input.mousePosition);
			}
		}
	}

	private void FixedUpdate()
	{
		if (selectedBlock != null)
		{
			updateMovementDirectionForPhone();
			selectedBlock.transform.position += phoneMovement * Time.fixedDeltaTime;
		}
	}

	private void checkTouchOnBlockObject(Vector2 touchPosition)
	{
		Ray raycast = Camera.main.ScreenPointToRay(touchPosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(raycast, out raycastHit))
		{
			Debug.Log("Something Hit");
			if (raycastHit.collider.CompareTag("Block"))
			{
				Debug.Log("Clicked: " + raycastHit.collider.name);
				raycastHit.collider.GetComponent<Renderer>().material.color = Color.green;
			}
		}
	}

	private void updateMovementDirectionForPhone()
	{
		gravity = 0.9f * gravity + 0.1f * Input.acceleration;

		var acceleration = Input.gyro.userAcceleration;
		acceleration = new Vector3(-acceleration.x, 0, acceleration.z);
		phoneMovement = acceleration * Time.fixedDeltaTime;

		phoneMovement *= 0.999f;

		Debug.Log("Velocity[Phone]: " + phoneMovement);
	}
}
