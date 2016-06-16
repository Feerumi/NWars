using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KnockBack : MonoBehaviour {

	Rigidbody rigidBody;
	float explosionForce = 0;
	public float explosionRadius;
	public Text playerOneText;
	public Text playerTwoText;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

	}

	void OnCollisionEnter(Collision other){

		if (other.gameObject.tag.Equals ("ThrowableObject")) {
			explosionForce += 10;
			if (this.gameObject.name.Equals ("Player1")) {
				Debug.Log ("Player1" + explosionForce);
				playerOneText.text = explosionForce;
			} else if (this.gameObject.name.Equals("Player2")) {
				Debug.Log ("Player2" + explosionForce);
				playerTwoText.text = explosionForce;
			}

			ThrowableKnockBack values = other.gameObject.GetComponent<ThrowableKnockBack> ();
			if (values.getActive()) {
				rigidBody.AddExplosionForce (explosionForce * values.getMass(), other.transform.position, explosionRadius);	
			}
		}
	}
}
