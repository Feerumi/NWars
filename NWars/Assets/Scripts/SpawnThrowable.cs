using UnityEngine;
using System.Collections;

public class SpawnThrowable : MonoBehaviour {

	/**
	 * 3D space in which objects are spawned.
	 */
	private Vector3 spawnArea;

	/**
	 * If no parent is set, fallback value for the extent of X dimension.
	 */
	public float spawnAreaX;

	/**
	 * If no parent is set, fallback value for the extent of Y dimension.
	 */
	public float spawnAreaY;

	/**
	 * If no parent is set, fallback value for the extent of Z dimension.
	 */
	public float spawnAreaZ;

	/**
	 * Empty space around the spawn area X axis.
	 */
	public float paddingX;

	/**
	 * Empty space around the spawn area Y axis.
	 */
	public float paddingY;

	/**
	 * Empty space around the spawn area Z axis.
	 */
	public float paddingZ;

	/**
	 * Type of object to spawn.
	 */
	public GameObject objectToSpawn;

	/**
	 * How frequently a new object is spawned.
	 */
	public float spawnInterval;

	/**
	 * Minimum scale offset.
	 */
	public float scaleMin;

	/**
	 * Maximum scale offset.
	 */
	public float scaleMax;

	// Use this for initialization
	void Start () {
		// Determine extents of spawn area either based on parent dimensions, or
		// through input values if no parent is present.
		if (transform.parent != null) {
			spawnArea = transform.parent.GetComponent<Renderer> ().bounds.extents;	
		} else {
			spawnArea = new Vector3 (spawnAreaX, 0, spawnAreaZ);
		}

		// Apply padding.
		spawnArea.x = spawnArea.x - paddingX;
		spawnArea.y = spawnArea.y - paddingY;
		spawnArea.z = spawnArea.z - paddingX;

		InvokeRepeating ("spawnObject", 0.7f, spawnInterval);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnObject() {
		// Spawn a new item with random Y axis rotation.
		GameObject instance = (GameObject)Instantiate (objectToSpawn, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));

		// Randomize position.
		Vector3 pos = instance.transform.position;
		pos.x += Random.Range (-spawnArea.x, spawnArea.x);
		pos.z += Random.Range (-spawnArea.z, spawnArea.z);
		instance.transform.position = pos;

		// Randomize size.
		float resize = Random.Range (scaleMin, scaleMax);
		instance.transform.localScale += new Vector3 (resize, resize, resize);
	}
}
