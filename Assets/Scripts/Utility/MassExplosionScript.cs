using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassExplosionScript : MonoBehaviour {

	public float radius = 5;
	public float power = 20;

	void Start()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);

		foreach (Collider c in colliders)
		{
			if (c.GetComponent<Rigidbody>() == null) continue;

			c.attachedRigidbody.AddExplosionForce (power, transform.position, radius, 1, ForceMode.Impulse);
		}
	}
}
