using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KnockBack : MonoBehaviour {

	Rigidbody rigidBody;
	float explosionForce = 200;
	public float explosionRadius;
	public Text playerOneText;
	public Text playerTwoText;
	float p1multiplier;
	float p2multiplier;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		p1multiplier = 0;
		p2multiplier = 0;
	}

	void OnCollisionEnter(Collision other){

		ThrowableKnockBack values = other.gameObject.GetComponent<ThrowableKnockBack> ();

		if (other.gameObject.tag.Equals ("ThrowableObject") && values.getActive()) {

			if (this.gameObject.name.Equals ("Player1")) {
				Debug.Log ("Player1" + explosionForce);
				p1multiplier += other.impulse.magnitude * 10;
				playerOneText.text = Mathf.Round(p1multiplier).ToString() + "%";
				rigidBody.AddExplosionForce (explosionForce * values.getMass() * (1 + p1multiplier / 100), other.transform.position, explosionRadius);
			} else if (this.gameObject.name.Equals("Player2")) {
				Debug.Log ("Player2" + explosionForce);
				p2multiplier += other.impulse.magnitude * 10;
				playerTwoText.text = Mathf.Round(p2multiplier).ToString() + "%";
				rigidBody.AddExplosionForce (explosionForce * values.getMass() * (1 + p2multiplier / 100), other.transform.position, explosionRadius);
			}
			values.setPassive ();
		}
	}
}