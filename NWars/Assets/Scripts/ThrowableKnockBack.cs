using UnityEngine;
using System.Collections;

public class ThrowableKnockBack : MonoBehaviour {

	public bool isActive;
	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	public bool getActive(){
		return isActive;
	}

	public float getMass(){
		return rigidBody.mass;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
