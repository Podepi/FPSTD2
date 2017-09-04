using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

	public float timer = 1.0f;
	public Vector3 target;
	LineRenderer lineRend;
	float timeLeft = 1.0f;

	private void Start()
	{
		lineRend = GetComponent<LineRenderer> ();
		
		//Destroy(gameObject, timer);

		lineRend.SetPosition (0, transform.position);
		lineRend.SetPosition (1, target);
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft = Mathf.Clamp01(timeLeft - Time.deltaTime / timer);
		lineRend.widthMultiplier = timeLeft;
		lineRend.SetPosition (0, transform.position);
		lineRend.SetPosition (1, target);
		//float alpha = Mathf.Lerp(0, 255, timeLeft);
		//Color c = new Color(1f, 1f, 1f, alpha);
		//lineRend.startColor = c;
		//lineRend.endColor = c;
	}

	public void SetTarget(Vector3 newTarget) {
		target = newTarget;
		lineRend = GetComponent<LineRenderer> ();
		lineRend.SetPosition (0, transform.position);
		lineRend.SetPosition (1, target);
	}
}
