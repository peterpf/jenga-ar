using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class TowerController : MonoBehaviour
{
    // The height of the tower
    public int layerHeight = 12;
    
	// Color of the blocks when selected
	public Material selectMaterial;

	// Defaul block color
	public Material defaultMaterial;
	public float thrust = 2.0f;

	// Factor to amplify accelerometer readings
	public float thrust = 350f;

	// Selected block of the tower
	public GameObject selectedBlock;

	// Previously selected block
	private GameObject prevSelectedBlock = null;

	// Raycast used for detecting 'click' on object
	private RaycastHit raycastHit;

	// Indicated whether two fingers touch the display
	private bool twofingerClick = false;


	void Start ()
	{
		Debug.Log ("Started");

		Input.gyro.enabled = true;
		initTower ();
	}

	private void initTower() {
		for (int i = 0; i < layerHeight; i++) {
			GameObject layer = Instantiate(layerPrefab);
			RectTransform rt = (RectTransform)layer.transform;
			layer.transform.position =  new Vector3 (
				gameObject.transform.position.x,
				gameObject.transform.position.y,
				gameObject.transform.position.z);
			layer.transform.position.y = rt.rect.height * i + 0.01f;
			layer.transform.eulerAngles = new Vector3(
				layer.transform.eulerAngles.x,
				layer.transform.eulerAngles.y + 180f * i,
				layer.transform.eulerAngles.z
			);
			layer.transform.parent = gameObject.transform;
			Debug.Log ("Created Object at position:" + layer.transform.position);
		}
	}


	/**
	 * Resets the material of the given object to the specified defaultMaterial
	 **/
	private void resetMaterialForObject (GameObject obj)
	{
		obj.GetComponent<MeshRenderer> ().material = defaultMaterial;
	}


	/**
	 * Handles clicking on the screen as well as object-selection
	 **/
	private void handleClick (Vector3 pos)
	{
		Ray raycast = Camera.main.ScreenPointToRay (pos);

		// Check if any object is clicked
		if (Physics.Raycast (raycast, out raycastHit)) {
			
			Debug.Log ("Hit something");
			
			GameObject hitObject = raycastHit.transform.gameObject;

			// Check if hitObject is the same as the selectedObject - unselect if true
			if (hitObject.Equals (selectedBlock)) {
				Debug.Log ("Deselecting selectedBlock");
				resetMaterialForObject (selectedBlock);
				selectedBlock = null;

				// Check if the hitObject is a block and select it, unselect the previous one
			} else if (hitObject.CompareTag ("Block")) {
				Debug.Log ("Selecting block");
				selectedBlock = hitObject;

				if (prevSelectedBlock != null) {
					resetMaterialForObject (prevSelectedBlock);
				}

				selectedBlock.GetComponent<MeshRenderer> ().material = selectMaterial;
			}
			prevSelectedBlock = selectedBlock;
		}
	}


	void Update ()
	{
		// Check if platform is non-android and perform default operations
		if (Application.platform != RuntimePlatform.Android) {

			if (Input.GetMouseButtonDown (0)) {
				handleClick (Input.mousePosition);
			}
			return;
		}

		// Platform is Android - handle gracefully with multi-touch

		if ((Input.touchCount == 1) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
			Debug.Log ("Only one touch detected");
			handleClick (Input.GetTouch (0).position);
		}

		twofingerClick = (Input.touchCount > 1) &&
		(Input.GetTouch (0).phase == TouchPhase.Stationary || Input.GetTouch (0).phase == TouchPhase.Moved) &&
		(Input.GetTouch (1).phase == TouchPhase.Stationary || Input.GetTouch (1).phase == TouchPhase.Moved);

	}

	
	void FixedUpdate ()
	{
		// Update movement of selectedBlock (if two touches detected)
		if (twofingerClick && selectedBlock != null) {
			var acceleration = Input.gyro.userAcceleration;
			acceleration = new Vector3 (-acceleration.x, 0, acceleration.z);
			selectedBlock.transform.GetComponent<Rigidbody> ().AddForce (acceleration * thrust);
		}
		
	}
}
