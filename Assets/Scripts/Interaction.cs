using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO:
-Fix object carrying mechanic
*/

public class Interaction : MonoBehaviour {

	private bool ComputerState;
	private bool CarryingObject;

	// Use this for initialization
	void Start () {
		ComputerState = false;
		CarryingObject = false;
	}
	
	// Update is called once per frame
	void Update () {
           //Bit shift index of interactable object layer to get bit mask
           int InteractableLayerMask = 1 << 8;
           
           RaycastHit Hit;
           //Ray PlayerSightLine = new Ray(transform.position, transform.forward);
           Debug.DrawRay(transform.position, transform.forward);

           //check raycast collisions with interactable objects
           if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 20.0f, InteractableLayerMask))
           {
           	Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * Hit.distance, Color.red);
           	//Debug.Log("Did hit computer");
           	
           	Debug.Log(Hit.collider.gameObject.name);
           		if(Input.GetButtonDown("Interact"))
           		{
           			//if it's the computer switch states...
           			if(Hit.collider.gameObject.name.Equals("Computer Object") || Hit.collider.gameObject.name.Equals("Computer Screen"))
           			{
           				//test code
           				ComputerState = !ComputerState;
           			}
           			//...if not then pick up the object
           			else
           			{
           				CarryingObject = !CarryingObject;
           				//Debug.Log("PICKUP " + Hit.collider.gameObject.name);
           			}
           		}
           }
           else
           {
           	Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * Hit.distance, Color.black);
           	//Debug.Log("Did NOT hit computer");
           }

           Debug.Log("COMPUTER STATE = " + ComputerState);
           if(CarryingObject)
           {
           	PickupObject(Hit.collider.gameObject);
           }
	}

	void PickupObject(GameObject Object)
	{
		Object.GetComponent<Rigidbody>().isKinematic = true;
		Object.transform.position = Vector3.Lerp(Object.transform.position, transform.position, 0.5f);
	}
}