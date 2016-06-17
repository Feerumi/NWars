using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {


	public GameObject PickUpItemPosition;

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

	public string pickUpKey;
	public string throwKey;
	private float maxThrowCharge = 1;

	/**
     * Percent at which charge begins.
	 */
	public float minCharge;

	/**
	 * Time in seconds it takes to fully charge the throw.
	 */
	public float rechargeRate;

	/**
	 * Current throw charge.
	 */
	private float throwCharge;

	/**
	 * Is the player in the proccess of charging a throw.
	 */
	private bool charging = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		//Check if item is in range and if pickup item button is pressed.
		if (!ItemPickedUp && ItemInRange && Input.GetButtonDown (pickUpKey)) {
			//Debug.Log ("Picked up item");

			//Set items position as player pickup item position
			item.transform.position = this.gameObject.transform.position;
			item.transform.rotation = this.gameObject.transform.rotation;
			item.transform.position = new Vector3 (item.transform.position.x + 1f, item.transform.position.y, item.transform.position.z);

			//Set gravity to false so that while item is in players hand its not affected by gravity.
			item.GetComponent<Rigidbody> ().useGravity = false;

			//Item is picked up.
			ItemPickedUp = true;

		//If an item is picked up and the throw key is held down, charge the throw.
		} else if (ItemPickedUp && Input.GetButtonDown(pickUpKey) || charging) {
			charging = true;

			// Upon key release, throw the object.
			if (Input.GetButtonUp (pickUpKey)) {

				//Make item non kinematic and make it be affected by gravity.
				item.GetComponent<Rigidbody> ().isKinematic = false;
				item.GetComponent<Rigidbody> ().useGravity = true;
				item.GetComponent<BoxCollider> ().isTrigger = false;
				StartCoroutine ("Delay");

				item.GetComponent<Rigidbody> ().AddForce (PickUpItemPosition.transform.forward * VerticalForceAmount * throwCharge);
				item.GetComponent<Rigidbody> ().AddForce (PickUpItemPosition.transform.up * HorizontalForceAmount * throwCharge);
				item.GetComponent<Rigidbody> ().AddTorque (new Vector3 (Random.Range (-10, 10), Random.Range (-10, 10), Random.Range (-10, 10)));
				ItemPickedUp = false;
				throwCharge = minCharge;
				charging = false;
			// Otherwise charge the throw.
			} else if (Input.GetButton(pickUpKey)) {
				throwCharge = Mathf.Clamp(throwCharge + (Time.deltaTime / rechargeRate * minCharge), minCharge, maxThrowCharge);
			}
		}
			
		if (ItemPickedUp) {
			item.transform.position = new Vector3 (PickUpItemPosition.transform.position.x
				, PickUpItemPosition.transform.position.y
				, PickUpItemPosition.transform.position.z);
			item.transform.rotation = PickUpItemPosition.transform.rotation;
		}
	}

	IEnumerator Delay() {
		yield return new WaitForSeconds(0.25f);
		item.GetComponent<ThrowableKnockBack> ().isActive = true;
	}
		
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "ThrowableObject") {
			Debug.Log ("Throwable item in range");
			if (!ItemPickedUp) {
				item = coll.gameObject;
				ItemInRange = true;
			}
		}
	}

	void OnTriggerExit(Collider coll) {
		Debug.Log ("Throwable item out of range");
		ItemInRange = false;
	}


		
}
