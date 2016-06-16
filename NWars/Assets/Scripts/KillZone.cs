using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class KillZone : MonoBehaviour {

	public List<GameObject> killList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		// Can the game object tag be found within the kill list.
		foreach(GameObject killable in killList) {
			if (collision.gameObject.tag.Equals (killable.tag)) {
				// Is the object is killable.
				Killable tmp = collision.gameObject.GetComponent<Killable>();

				if (tmp != null) tmp.onDeath();
			}
		}
	}
}
