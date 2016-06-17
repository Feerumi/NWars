using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour, Killable {


	public float movementSpeed;
	public float turningSpeed;
	public float dashMultiplier;
	public float dashDuration;
	private Vector3 direction = Vector3.zero;
	private Vector3 startPos;
	private Quaternion startRot;

	public bool isPlayerTwo;
	private bool dashing;
	private float dashSpeed;
	private float dashTimerInSeconds;
	private Rigidbody body;

	private string horizontalString;
	private string verticalString;
	public string dashString;

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
		startPos = gameObject.transform.position;
		startRot = gameObject.transform.rotation;
		setPlayerControls ();
		dashing = false;
		dashSpeed = movementSpeed * dashMultiplier;
		dashTimerInSeconds = 0f;
		body = GetComponent<Rigidbody> ();
		horizontalSpeed = new Vector3 (0, 0, 0);
	}

	void setPlayerControls(){

		if (isPlayerTwo) {
			horizontalString = "Horizontal2";
			verticalString = "Vertical2";
		} else {
			horizontalString = "Horizontal";
			verticalString = "Vertical";
		}

	}

	void FixedUpdate () {
		if (Input.GetButtonDown(dashString) && !dashing) {
			dashing = true;
			dashTimerInSeconds = dashDuration;
		}

		float horizontal = Input.GetAxisRaw (horizontalString);
		float vertical = Input.GetAxisRaw (verticalString);

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

	public void onDeath() {

		gameObject.transform.position = startPos;
		gameObject.transform.rotation = startRot;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<KnockBack> ().nullPercentage (isPlayerTwo);

	}
}
