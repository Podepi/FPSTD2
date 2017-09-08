using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public Transform target;
	private float waypointRadius = 2f;
	public float speed = 2f;

	private Vector3[] path;
	private int targetIndex;
	private NodeGrid grid;

	void Start() {
		grid = NodeGrid.instance;
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccess) {
		if (pathSuccess && newPath != null) {
			path = newPath;
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path [0];
		currentWaypoint.y = transform.position.y;

		while (true) {
			// If next waypoint is unwalkable, request new path
			if (NodeGrid.instance.NodeFromWorldPoint(currentWaypoint).walkable == false) {
				PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
			}
			// If within target radius
			if ((transform.position - currentWaypoint).magnitude <= waypointRadius) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					targetIndex = 0;
					path = new Vector3[0];
					yield break;
				}
				currentWaypoint = path [targetIndex];
				currentWaypoint.y = transform.position.y;
			}

			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed*Time.deltaTime);
			transform.LookAt (currentWaypoint);
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube (path [i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine (transform.position, path [i]);
				} else {
					Gizmos.DrawLine (path [i - 1], path [i]);
				}
			}
		}
	}
}
