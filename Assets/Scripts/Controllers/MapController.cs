using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	public int width = 5;
	public int height = 5;
	public int tileSize = 3;

	public Tile[,] tiles;

	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject permanentWallTile;

	// Use this for initialization
	void Start () {
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//void PlaceStructure (GameObject structureProto, int x, int z) {
	//	GameObject newStructure = Instantiate (structureProto, new Vector3 (x * tileSize, 0, z * tileSize), Quaternion.identity);
	//	tiles [x, z].SetStructure(newStructure);
	//}

	void GenerateMap() {
		Vector3 instantPoint = Vector3.zero;

		GenerateMapStatics ();

		// Generate inner tiles
		/*
		for (var x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				instantPoint.x = x * tileSize;
				instantPoint.z = z * tileSize;
				GameObject tile = Instantiate (floorTile, instantPoint, Quaternion.identity, transform);
				tile.transform.SetParent(transform);
				tile.name = "Tile " + x + ", " + z;
			}
		}*/
	}

	void GenerateMapStatics() {
		Vector3 instantPoint = Vector3.zero;
		// Generate outer walls

		// North wall
		instantPoint.x = width * tileSize / 2 - 1;
		instantPoint.z = -tileSize;
		GameObject NWall = Instantiate(permanentWallTile, instantPoint, Quaternion.identity, transform);
		NWall.transform.localScale = new Vector3 (height + 2f, 5f, 1f);
		NWall.name = "North Wall";

		// South wall
		instantPoint.x = width * tileSize / 2 - 1;
		instantPoint.z = height * tileSize;
		GameObject SWall = Instantiate(permanentWallTile, instantPoint, Quaternion.identity, transform);
		SWall.transform.localScale = new Vector3 (height + 2f, 5f, 1f);
		SWall.name = "South Wall";

		// West wall
		instantPoint.x = -tileSize;
		instantPoint.z = height * tileSize / 2 - 1;
		GameObject WWall = Instantiate(permanentWallTile, instantPoint, Quaternion.identity, transform);
		WWall.transform.localScale = new Vector3 (1f, 5f, width + 2f);
		WWall.name = "West Wall";

		// East wall
		instantPoint.x = width * tileSize;
		instantPoint.z = height * tileSize / 2 - 1;
		GameObject EWall = Instantiate(permanentWallTile, instantPoint, Quaternion.identity, transform);
		EWall.transform.localScale = new Vector3 (1f, 5f, width + 2f);
		EWall.name = "East Wall";

		// Generate floor
		instantPoint.x = width * tileSize / 2 - 1;
		instantPoint.z = height * tileSize / 2 - 1;
		GameObject floor = Instantiate (floorTile, instantPoint, Quaternion.identity, transform);
		floor.transform.localScale = new Vector3 (height + 2f, 1f, width + 2f);

	}

	void OnDrawGizmos() {
		//Gizmos.DrawCube(new Vector3(width*tileSize/2f, -0.5f, height*tileSize/2f), new Vector3(width*tileSize, 1f, height*tileSize));
	}
}
