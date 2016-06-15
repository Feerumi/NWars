using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour {

	Rigidbody rigidBody;
	public float explosionForce;
	public float explosionRadius;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision other){

		if (other.gameObject.tag.Equals ("ThrowableObject")) {
			ThrowableKnockBack values = other.gameObject.GetComponent<ThrowableKnockBack> ();
			if (values.getActive()) {
				rigidBody.AddExplosionForce (explosionForce * values.getMass(), other.transform.position, explosionRadius);	
			}
		}
	}
}
