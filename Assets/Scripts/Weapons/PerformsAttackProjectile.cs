using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformsAttackProjectile : MonoBehaviour {

	public float cooldown = 1f;
	float coolDownRemaining = 0;

	public GameObject projectilePrefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		coolDownRemaining -= Time.deltaTime;

		if (Input.GetMouseButton (1) && coolDownRemaining <= 0) {
			coolDownRemaining = cooldown;

			Instantiate (projectilePrefab, Camera.main.transform.position, Camera.main.transform.rotation);
		}
	}
}
