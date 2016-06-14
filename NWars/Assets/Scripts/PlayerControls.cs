using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public float movementSpeed;
	public float turningSpeed;
	public float dashMultiplier;
	public float dashDuration;
	private Vector3 direction = Vector3.zero;
	private CharacterController controller;
	private bool isMoving;

	private bool dashing;
	private float dashSpeed;
	private float dashTimerInSeconds;

	void Start () {
		controller = GetComponent<CharacterController> ();
		isMoving = false;
		dashing = false;
		dashSpeed = movementSpeed * dashMultiplier;
		dashTimerInSeconds = 0f;
	}

	void Update () {

		if (Input.GetButtonDown("Fire1") && !dashing) {
			dashing = true;
			dashTimerInSeconds = dashDuration;
		}

		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		if (horizontal != 0 || vertical != 0) {
			isMoving = true;
		}

		if (isMoving) {

			direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

			if (dashing) {
				direction *= dashSpeed;
			} else {
				direction *= movementSpeed;
			}

			if (direction != Vector3.zero) {
				transform.rotation = Quaternion.LookRotation (direction);
			}

			controller.Move (direction * Time.deltaTime);

		}

		if (dashTimerInSeconds > 0) {
			dashTimerInSeconds -= Time.deltaTime;
		} else {
			dashTimerInSeconds = 0;
			dashing = false;
		}

	}
}
