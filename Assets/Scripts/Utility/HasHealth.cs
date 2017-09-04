using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour {

	public float hitPoints = 100f;
	public GameObject debris;
	public Transform debrisPoint;

	void Start() {
		if (debrisPoint == null) {
			debrisPoint = transform;
		}
	}

	public void ReceiveDamage( float amount ) {
		hitPoints -= amount;
		Debug.Log (gameObject.name + " took " + amount + " damage");
		if (hitPoints <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy (gameObject);
		if (debris != null) {
			Instantiate (debris, debrisPoint.position, debrisPoint.rotation);
		}
	}
}
