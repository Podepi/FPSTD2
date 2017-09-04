using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_ThermalDetonator : MonoBehaviour {

	public GameObject explosionPrefab;

	float lifespan = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;

		if (lifespan <= 0) {
			Explode ();
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Enemy") {
			Destroy (collision.gameObject);
			Explode ();
		}
	}

	void Explode() {
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
