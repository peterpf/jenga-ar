using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class TowerController : MonoBehaviour
{
	public Material selectMaterial;
	public Material defaultMaterial;
	public float thrust = 200f;

	private GameObject prev = null;
	private RaycastHit raycastHit;
	private GameObject selectedBlock;

	private Vector3 gravity;
	private Vector3 phoneMovement;


	void Start ()
	{
		Input.gyro.enabled = true;
		Debug.Log ("Started");
	}


	private void handleClick (Ray raycast)
	{
		if (Physics.Raycast (raycast, out raycastHit)) {

			Debug.Log ("Something Hit");

			selectedBlock = raycastHit.transform.gameObject;

			// if (! block.CompareTag("NotBlock"))
			if (selectedBlock.CompareTag ("Block")) {

				if (prev != null) {
					prev.GetComponent<MeshRenderer> ().material = defaultMaterial;
				}

				selectedBlock.GetComponent<MeshRenderer> ().material = selectMaterial;
		
				prev = selectedBlock;
			}
		}
	}

	void Update ()
	{
		if ((Input.touchCount > 0) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
			Ray raycast = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			handleClick (raycast);
		}

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("MouseDown");
			Ray raycast = Camera.main.ScreenPointToRay (Input.mousePosition);
			handleClick (raycast);
		}
	}

	
	void FixedUpdate ()
	{
		if (selectedBlock != null) {
			var acceleration = Input.acceleration; //Input.gyro.userAcceleration;
			acceleration = new Vector3 (-acceleration.x, 0, acceleration.z);
			Debug.Log ("Applying force: " + acceleration * thrust);
			selectedBlock.transform.GetComponent<Rigidbody> ().AddForce (acceleration * thrust);
		}
	}

	private void checkTouchOnBlockObject (Vector3 pos)
	{
		Vector3 wp = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint (touchPos);

		Debug.Log ("clicked on " + hit.GetComponent<Collider> ());

		if (hit && hit == gameObject.GetComponent<Collider2D> ()) {
			selectedBlock = hit.gameObject;
		}
	}
}