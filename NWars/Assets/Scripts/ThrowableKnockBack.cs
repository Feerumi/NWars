using UnityEngine;
using System.Collections;

public class ThrowableKnockBack : MonoBehaviour {
	
	public bool isActive;
	public float killTime;
	bool destroyCalled;
	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		destroyCalled = false;
	}

	public bool getActive(){
		return isActive;
	}

	public void setPassive(){
		isActive = false;
		if (!destroyCalled) {
			Destroy (this.gameObject, killTime);
		}
	}

	public float getMass(){
		return rigidBody.mass;
	}

	void OnCollisionEnter(Collision other){

		if (other.gameObject.tag.Equals ("Platform")) {

			if (isActive) {
				isActive = false;
				setPassive ();
			}

		}

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void onDeath(){
		if (!destroyCalled) {
			Destroy (this.gameObject, killTime);
		}
	}
}