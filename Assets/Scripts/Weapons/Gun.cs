using System;
using UnityEngine;

public class Gun : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public float cooldown = 0.5f;
	float coolDownRemaining = 0;

	public Camera fpsCam;

	public GameObject hitEffectPrefab;
	public GameObject lineEffectPrefab;
	public float lineWidth = 1.0f;
	public Transform shootPoint;

	// Collide with all layers except the player
	int layerMask = ~(1 << 8);
	
	// Update is called once per frame
	void Update () {
		coolDownRemaining -= Time.deltaTime;
		if (Input.GetAxis ("Fire1") > 0f && coolDownRemaining <= 0f) {
			coolDownRemaining = cooldown;
			Shoot ();
		}
	}

	void Shoot() {
		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask)) {
			GameObject go = hit.collider.gameObject;

			// Attempt to grab gameobject of the rigidbody attached to the collider, if it exists.
			// This is so we can hit compound colliders.
			try {
				go = hit.collider.attachedRigidbody.gameObject;
			} catch (NullReferenceException e) {}

			OnBulletHit (go, hit);
		}

		// Show laser effect. This is after the if statement because we want to see the laser even if it hits nothing.
		if (lineEffectPrefab != null) {
			GameObject laser = Instantiate (lineEffectPrefab, shootPoint.position, Quaternion.identity);
			laser.transform.SetParent (transform);
			laser.GetComponent<LaserScript> ().lineWidth = lineWidth;
			if (hit.collider == null) {
				// Laser missed
				laser.GetComponent<LaserScript> ().SetTarget (fpsCam.transform.position + (fpsCam.transform.forward * range));
			} else {
				// Laser hit
				laser.GetComponent<LaserScript> ().SetTarget (hit.point);
			}
		}
	}

	void OnBulletHit(GameObject go, RaycastHit hit) {
		HasHealth h = go.GetComponent<HasHealth> ();
		Rigidbody rb = go.GetComponent<Rigidbody> ();

		// Deal damage
		if (h != null) {
			h.ReceiveDamage (damage);
		}

		// Apply push to rigidbody
		if (rb != null) {
			Debug.Log ("Applying laser push to " + go.name);
			rb.AddForceAtPosition (fpsCam.transform.forward * damage, hit.point, ForceMode.Impulse);
		}

		// Show impact effect
		if (hitEffectPrefab != null) {
			Instantiate (hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
		}
	}
}
