using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public GameObject[] unitsToSpawn;
	public float spawnTime = 5f;
	public Transform target;

	float nextSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		nextSpawn -= Time.deltaTime;
		if (nextSpawn <= 0) {
			nextSpawn = spawnTime;
			int newUnitIndex = Random.Range (0, unitsToSpawn.Length - 1);
			GameObject newUnit = Instantiate (unitsToSpawn [newUnitIndex], transform.position, Quaternion.identity);
			newUnit.GetComponent<Unit> ().target = target;
		}
	}
}
