using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

	public float timer = 1.0f;

	private void Start()
	{
		Destroy(gameObject, timer); 
	}
}
