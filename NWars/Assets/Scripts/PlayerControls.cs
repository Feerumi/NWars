using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {


	public float movementSpeed;
	public float turningSpeed;
	public float dashMultiplier;
	public float dashDuration;
	private Vector3 direction = Vector3.zero;
	private CharacterController controller;

	private bool dashing;
	private float dashSpeed;
	private float dashTimerInSeconds;
	private Rigidbody body;

	/**
	 * Used to calculate the players movement speed alongthe X and Z axis, as
	 * Y axis should not affect player movement calculations.
	 */
	private Vector3 horizontalSpeed;

	/**
	 * Maximum velocity player can apply to the game object.
	 * 
	 * Does not limit the maximum speed that can be achieved as
	 * a result of collision.
	 */
	public float maxSpeed;

	public float maxSpeedDash;

	/**
	 * Maximum force the player can apply to the player object
	 * during a single update pass. Effectively controls how quickly
	 * player reaches max speed.
	 */
	public float maxForce;

	/**
	 * Force that is applied during a dash.
	 */
	public float maxForceDash;

	void Start () {
		controller = GetComponent<CharacterController> ();
		dashing = false;
		dashSpeed = movementSpeed * dashMultiplier;
		dashTimerInSeconds = 0f;
		body = GetComponent<Rigidbody> ();
		horizontalSpeed = new Vector3 (0, 0, 0);
	}

	void FixedUpdate () {

		if (Input.GetButtonDown("Fire1") && !dashing) {
			dashing = true;
			dashTimerInSeconds = dashDuration;
		}

		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		if (horizontal != 0 || vertical != 0) {

			horizontalSpeed.x = body.velocity.x;
			horizontalSpeed.z = body.velocity.z;

			// Rotate player to the direction of player input 
			transform.rotation = Quaternion.Lerp(transform.rotation, 
				Quaternion.LookRotation(new Vector3(horizontal, 0, vertical)), 
				turningSpeed);



			if (dashing) {

				// Limits the maximum force.
				if (Mathf.Sign (horizontal) == Mathf.Sign(body.velocity.x) && horizontalSpeed.magnitude >= maxSpeedDash)
					horizontal = 0;

				if (Mathf.Sign (vertical) == Mathf.Sign(body.velocity.z) && horizontalSpeed.magnitude >= maxSpeedDash)
					vertical = 0;


				direction = new Vector3 (horizontal, 0, vertical);

				direction *= dashSpeed;
				body.AddForce( Vector3.ClampMagnitude(direction, maxForceDash));
			} else {

				// Limits the maximum force.
				if (Mathf.Sign (horizontal) == Mathf.Sign(body.velocity.x) && horizontalSpeed.magnitude >= maxSpeed)
					horizontal = 0;

				if (Mathf.Sign (vertical) == Mathf.Sign(body.velocity.z) && horizontalSpeed.magnitude >= maxSpeed)
					vertical = 0;

				direction = new Vector3 (horizontal, 0, vertical);

				direction *= movementSpeed;
				body.AddForce (Vector3.ClampMagnitude (direction, maxForce));
			}
		}

		if (dashTimerInSeconds > 0) {
			dashTimerInSeconds -= Time.deltaTime;
		} else {
			dashTimerInSeconds = 0;
			dashing = false;
		}

	}
}
