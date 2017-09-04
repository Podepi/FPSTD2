using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformsAttack : MonoBehaviour {

	public float range = 100.0f;
	public float damage = 10.0f;

	// Collide with all layers except the player
	int layerMask = ~(1 << 8);

	public float cooldown = 0.5f;
	float coolDownRemaining = 0;

	public GameObject hitEffectPrefab;
	public GameObject lineEffectPrefab;
	public float lineWidth = 1.0f;
	public Transform shootPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		coolDownRemaining -= Time.deltaTime;

		if (Input.GetMouseButton (0) && coolDownRemaining <= 0) {
			coolDownRemaining = cooldown;
			Transform cmt = Camera.main.transform;

			Ray ray = new Ray (cmt.position, cmt.forward);
			RaycastHit hitInfo;

			if (Physics.Raycast (ray, out hitInfo, range, layerMask)) {
				Vector3 hitPoint = hitInfo.point;
				GameObject go = hitInfo.collider.gameObject;

				// Attempt to grab gameobject of attached rigidbody.
				// This is so we can hit compound colliders.
				try {
					go = hitInfo.collider.attachedRigidbody.gameObject;
				} catch (NullReferenceException e) {}

				HasHealth h = go.GetComponent<HasHealth> ();
				Rigidbody rb = go.GetComponent<Rigidbody> ();

				// Deal damage
				if (h != null) {
					h.ReceiveDamage (damage);
				}

				// Apply push to rigidbody
				if (rb != null) {
					Debug.Log ("Applying laser push to " + go.name);
					rb.AddForceAtPosition (cmt.forward * damage, hitPoint, ForceMode.Impulse);
				}

				// Show impact effect
				if (hitEffectPrefab != null) {
					Instantiate (hitEffectPrefab, hitPoint, Quaternion.LookRotation(hitInfo.normal));
				}
			}
			// Show laser effect. This is after the if statement because we want to see the laser even if it hits nothing.
			if (lineEffectPrefab != null) {
				GameObject laser = Instantiate (lineEffectPrefab, shootPoint.position, Quaternion.identity);
				laser.transform.SetParent (transform);
				laser.GetComponent<LaserScript> ().lineWidth = lineWidth;
				if (hitInfo.point == Vector3.zero) {
					// Laser missed
					laser.GetComponent<LaserScript> ().SetTarget (cmt.position + (cmt.forward * range));
				} else {
					laser.GetComponent<LaserScript> ().SetTarget (hitInfo.point);
				}
			}
		}
	}
}
