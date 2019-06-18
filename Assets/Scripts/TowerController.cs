using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class TowerController : MonoBehaviour
{
	public int layerHeight = 12;

	public GameObject layerPrefab;
	public Material selectMaterial;
	public Material defaultMaterial;
	public float thrust = 400f;

	// Selected block of the tower
	public GameObject selectedBlock;
	private GameObject prevSelectedBlock = null;

	private RaycastHit raycastHit;


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


	private void handleClick (Vector3 pos)
	{
		Ray raycast = Camera.main.ScreenPointToRay (pos);

		if (Physics.Raycast (raycast, out raycastHit)) {
			
			Debug.Log ("Hit something");
			
			GameObject hitObject = raycastHit.transform.gameObject;

			// Check if hitObject is the same as the selectedObject - unselect if true
			if (hitObject.Equals (selectedBlock)) {
				Debug.Log ("Deselecting selectedBlock");
				resetMaterialForObject (selectedBlock);
				selectedBlock = null;

				// Check if the hitObject is a block and select it
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
		if ((Input.touchCount > 0) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
			handleClick (Input.GetTouch (0).position);
		} else if (Input.GetMouseButtonDown (0)) {
			handleClick (Input.mousePosition);
		}
	}

	
	void FixedUpdate ()
	{
		// Update movement of selectedBlock
		if (selectedBlock != null) {
			var acceleration = Input.gyro.userAcceleration;
			acceleration = new Vector3 (-acceleration.x, 0, acceleration.z);
			selectedBlock.transform.GetComponent<Rigidbody> ().AddForce (acceleration * thrust);
		}
	}
}
