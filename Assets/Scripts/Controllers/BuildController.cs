using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {

	public List<buildObjects> objects = new List<buildObjects>();
	public buildObjects currentObject;
	public Transform previewPoint;
	public LayerMask layerMask;
	public float buildRange = 10f;

	private Vector3 currentPosition;
	private RaycastHit hit;
	private NodeGrid grid;

	// Use this for initialization
	void Start () {
		currentObject = objects [0];
		ChangeCurrentBuilding ();
		grid = NodeGrid.instance;
	}

	// Update is called once per frame
	void Update () {
		Transform cam = Camera.main.transform;
		RaycastHit hit;
		Node node;
		if (Physics.Raycast (cam.position, cam.forward, out hit, buildRange, layerMask)) {
			node = grid.NodeFromWorldPoint (hit.point + hit.normal);
			previewPoint.gameObject.SetActive (true);
			previewPoint.position = node.worldPosition;

			// Mouse input
			if (Input.GetMouseButtonDown (0)) {
				grid.PlaceStructure (currentObject.gameObject, previewPoint.position, Quaternion.identity);
			} else if (Input.GetMouseButtonDown (1)) {
				grid.RemoveStructure (hit.point - hit.normal);
			}

			//Debug.DrawRay (hit.point, hit.normal, Color.red);
		} else {
			previewPoint.gameObject.SetActive (false);
		}
	}

	public void ChangeCurrentBuilding() {
		GameObject preview = Instantiate (currentObject.preview, currentPosition, Quaternion.identity) as GameObject;
		previewPoint = preview.transform;
	}
}

[System.Serializable]
public class buildObjects
{
	public string name;
	public GameObject preview;
	public GameObject gameObject;
	public int cost;
}