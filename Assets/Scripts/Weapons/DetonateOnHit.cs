using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonateOnHit : MonoBehaviour {

	public GameObject explosionPrefab;

	public float damage = 200f; // Damage at center of explosion
	public float explosionRadius = 3f;

	void OnTriggerEnter(Collider hit) {
		Explode (hit);
	}

	void Explode(Collider hit) {
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);

		foreach (Collider c in colliders) {

			GameObject go;

			// Attempt to grab gameobject of attached rigidbody.
			// This is so we can hit compound colliders.
			try {
				go = c.attachedRigidbody.gameObject;
			} catch(NullReferenceException e) {
				go = c.gameObject;
			}

			HasHealth h = go.GetComponent<HasHealth> ();

			// Skip if collider doesn't have health
			if (h == null) continue;

			// Deal full damage if collider was hit directly
			if (c == hit) {
				h.ReceiveDamage (damage);
				continue;
			}
			// Otherwise, deal damage based on distance.

			// Damage ratio clamped so that damage doesn't go below 0.
			float dist = Vector3.Distance (transform.position, go.transform.position);
			float damageRatio = Mathf.Clamp01(1f - (dist / explosionRadius));

			h.ReceiveDamage (damage * damageRatio);
		}

		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
