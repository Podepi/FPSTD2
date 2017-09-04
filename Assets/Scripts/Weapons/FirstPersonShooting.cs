using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShooting : MonoBehaviour {

	public GameObject bulletPrefab;
	float bulletImpulse = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			GameObject newBullet = Instantiate (bulletPrefab, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
			newBullet.GetComponent<Rigidbody>().AddForce (bulletImpulse * newBullet.transform.forward, ForceMode.Impulse);
		}
	}
}
