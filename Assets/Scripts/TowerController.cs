using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

	// Use this for initialization
	public Material selectMaterial;
	public Material defaultMaterial;
	private GameObject prev = null;


	Vector3 gravity;
	Vector3 phoneMovement;

	void Start () {
		//ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.Log ("Strarted");
		Input.gyro.enabled = true;
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



	void FixedUpdate()
	{
	    if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
	    {
	        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
	        RaycastHit raycastHit;
	        GameObject block;
	       if (Physics.Raycast(raycast, out raycastHit))
	        {
	        	block = raycastHit.transform.gameObject;
	            Debug.Log("Something Hit");
	            
	            if (! block.CompareTag("NotBlock"))
	            //if (block.CompareTag("Block"))
	            {
	            	if (prev != null)
		            {
		            	prev.GetComponent<MeshRenderer> ().material = defaultMaterial;
		            }

		            block.GetComponent<MeshRenderer> ().material = selectMaterial;

		            prev = block;

		            phoneMovement = new Vector3(1.0f,-2.0f,1.0f);
		            block.GetComponent<Rigidbody>().AddForce(phoneMovement);//, ForceMode.VelocityChange);
	            }

	          
	        }
	    }


	     if (Input.GetMouseButtonDown (0)) 
	     {
         	Debug.Log ("MouseDown");
        	Ray raycast = Camera.main.ScreenPointToRay (Input.mousePosition); 
	        RaycastHit raycastHit;
		    GameObject block;
	        if (Physics.Raycast(raycast, out raycastHit))
	        {
	        	block = raycastHit.transform.gameObject;
	            Debug.Log("Something Hit");
	            
	            if (! block.CompareTag("NotBlock"))
	            //if (block.CompareTag("Block"))
	            {
	            	if (prev != null)
		            {
		            	prev.GetComponent<MeshRenderer> ().material = defaultMaterial;
		            }

		            block.GetComponent<MeshRenderer> ().material = selectMaterial;

		            prev = block;

		            phoneMovement = new Vector3(1.0f,0.0f,0.0f);
		            block.GetComponent<Rigidbody>().AddForce(phoneMovement * 200); //, ForceMode.Acceleration);
	            }

	          
	        }




	     if (Input.GetMouseButtonDown (1)) 
	     {
         	Debug.Log ("MouseDown");
        	raycast = Camera.main.ScreenPointToRay (Input.mousePosition); 
	        if (Physics.Raycast(raycast, out raycastHit))
	        {
	        	block = raycastHit.transform.gameObject;
	            Debug.Log("Something Hit");
	            
	            if (! block.CompareTag("NotBlock"))
	            //if (block.CompareTag("Block"))
	            {
	            	if (prev != null)
		            {
		            	prev.GetComponent<MeshRenderer> ().material = defaultMaterial;
		            }

		            block.GetComponent<MeshRenderer> ().material = selectMaterial;

		            prev = block;

		            phoneMovement = new Vector3(-1.0f,0.0f,0.0f);
		            block.GetComponent<Rigidbody>().AddForce(phoneMovement * 200); //, ForceMode.Acceleration);
	            }

	          
	        }
	    }
	}
    
  }
}
