using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class TowerController : MonoBehaviour
{

	// Use this for initialization

	public Material selectMaterial;

	public Material defaultMaterial;

	private GameObject prev = null;


	void Start ()
	{
		Debug.Log ("Strarted");
	}


	private void handleClick(Ray raycast) {
		RaycastHit raycastHit;

		GameObject block;

		if (Physics.Raycast (raycast, out raycastHit)) {

			block = raycastHit.transform.gameObject;

			Debug.Log ("Something Hit");



			//if (! block.CompareTag("NotBlock"))

			/*if (block.CompareTag("Block"))

                {*/

			if (prev != null) {
				prev.GetComponent<MeshRenderer> ().material = defaultMaterial;
			}
			block.GetComponent<MeshRenderer> ().material = selectMaterial;

			prev = block;

			//}

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
}