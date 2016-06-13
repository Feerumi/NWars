using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public float movementSpeed;
	public float turningSpeed;
	private Vector3 direction = Vector3.zero;
	private CharacterController controller;
	private bool isMoving;

	void Start () {
		controller = GetComponent<CharacterController> ();
		isMoving = false;
	}

	void Update () {
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		if (horizontal != 0 || vertical != 0) {
			isMoving = true;
		}

		if (isMoving) {

			direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			direction *= movementSpeed;

			if (direction != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation (direction);
			}

			controller.Move (direction * Time.deltaTime);


		}
	}
}
