using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour {

	public bool displayGridGizmos;
	public Transform player;
	public LayerMask unWalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake() {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	public int MaxSize{
		get {
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = new Vector3(
					x * nodeDiameter + transform.position.x,
					transform.position.y,
					y * nodeDiameter + transform.position.z);
				
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius*0.8f, unWalkableMask));

				grid [x, y] = new Node (walkable, worldPoint, x, y);
			}
		}
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add (grid [checkX, checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		int x = Mathf.RoundToInt(worldPosition.x / nodeDiameter + transform.position.x);
		int y = Mathf.RoundToInt(worldPosition.z / nodeDiameter + transform.position.z);

		x = Mathf.Clamp(x, 0, gridSizeX-1);
		y = Mathf.Clamp(y, 0, gridSizeY-1);

		return grid [x, y];
	}

	void OnDrawGizmos() {
		
		Vector3 gridVectorSize = new Vector3(gridWorldSize.x, 1, gridWorldSize.y);
		Gizmos.DrawWireCube( gridVectorSize/2 + transform.position, gridVectorSize);

		if (grid != null && displayGridGizmos) {
			Node playerNode = NodeFromWorldPoint (player.position);
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;

				if (playerNode == n)
					Gizmos.color = Color.cyan;

				Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
			}
		}
	}
}
