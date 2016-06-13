using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	/**
	 * Used for checking if item is in players pickup range.
	 */
	bool ItemInRange = false;

	/**
	 * Used for checking if player has a item picked up.
	 */
	bool ItemPickedUp = false;

	/**
	 * Used for horizontal force when throwing item.
	 */
	public float HorizontalForceAmount;

	/**
	 * Used for vertical force when throwing item.
	 */
	public float VerticalForceAmount;

	/**
	 * Current nearby item.
	 */
	GameObject item;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		//Check if item is in range and if pickup item button is pressed.
		if ( !ItemPickedUp && ItemInRange && Input.GetKeyDown (KeyCode.R)) {
			Debug.Log ("Picked up item");

			//Set items position as player pickup item position
			item.transform.position = this.gameObject.transform.position;
			item.transform.rotation = this.gameObject.transform.rotation;
			item.transform.position = new Vector3 (item.transform.position.x + 1f , item.transform.position.y, item.transform.position.z);

			//Set gravity to false so that while item is in players hand its not affected by gravity.
			item.GetComponent<Rigidbody> ().useGravity = false;

			//Item is picked up.
			ItemPickedUp = true;

		}


		if (ItemPickedUp && Input.GetKeyDown (KeyCode.T)) {
			
			//Make item non kinematic and make it be affected by gravity.
			item.GetComponent<Rigidbody> ().isKinematic = false;
			item.GetComponent<Rigidbody> ().useGravity = true;
			item.GetComponent<BoxCollider> ().isTrigger = false;

			item.GetComponent<Rigidbody> ().AddForce (Vector3.up * VerticalForceAmount);
			item.GetComponent<Rigidbody> ().AddForce (Vector3.forward * HorizontalForceAmount);
			ItemPickedUp = false;
		}
	}


	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "ThrowableObject") {
			Debug.Log ("Throwable item in range");
			item = coll.gameObject;
			ItemInRange = true;
		}
	}

	void OnTriggerExit(Collider coll) {
		Debug.Log ("Throwable item out of range");
		ItemInRange = false;
	}


		
}
