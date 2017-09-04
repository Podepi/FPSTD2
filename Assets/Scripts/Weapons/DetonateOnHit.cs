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
			HasHealth h = c.GetComponent<HasHealth> ();

			// Skip if collider doesn't have health
			if (h == null) continue;

			// Deal full damage if collider was hit directly
			if (c == hit) {
				h.ReceiveDamage (damage);
				continue;
			}

			// Otherwise, deal damage based on distance.
			float dist = Vector3.Distance (transform.position, c.transform.position);

			// Clamped so that damage doesn't go below 0.
			float damageRatio = Mathf.Clamp(1f - (dist / explosionRadius), 0f, 1f);

			h.ReceiveDamage (damage * damageRatio);
		}

		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
