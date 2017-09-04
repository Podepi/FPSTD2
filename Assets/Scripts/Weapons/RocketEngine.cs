using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour {

	public float speed = 10f;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (transform.forward * speed * Time.deltaTime, Space.World);
	}
}
