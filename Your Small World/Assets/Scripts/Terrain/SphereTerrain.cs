using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class SphereTerrain : MonoBehaviour {

	//private List<QuadTree> sides;
	public int radius;
	public Vector3[] curVertices;
	public float[] heightMap;
	public bool[] buildableMap;
	public bool[] editableMap;
	public string[] biomeMap;

	public float maxHeight = 0.5f;
	public float minHeight = -0.5f;

	public const string DESERT_BIOME = "Desert";
	public const string LOW_BIOME = "Low";
	public const string MED_BIOME = "Medium";
	public const string HIGH_BIOME = "High";

	// Use this for initialization
	void Start () {
		/*sides = new List<QuadTree> ();
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.down, 1, Vector3.right, Vector3.forward));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.up, 1, Vector3.right, Vector3.forward));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.back, 1, Vector3.right, Vector3.up));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.forward, 1, Vector3.right, Vector3.up));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.left, 1, Vector3.forward, Vector3.up));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.right, 1, Vector3.forward, Vector3.up));*/
		Mesh planetMesh = GetComponent<MeshFilter> ().mesh;
		heightMap = new float[planetMesh.vertices.Length];
		buildableMap = new bool[planetMesh.vertices.Length];
		biomeMap = new string[planetMesh.vertices.Length];
		editableMap = new bool[planetMesh.vertices.Length];
		for (int i = 0; i < planetMesh.vertices.Length; i++) {
			heightMap[i] = 0.0f;
			buildableMap[i] = true;
			editableMap[i] = true;
			biomeMap[i] = "Desert";
		}
		updateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateMesh() {
		Mesh planetMesh = GetComponent<MeshFilter> ().mesh;
		planetMesh.vertices = expandCube (planetMesh.vertices, heightMap);
		curVertices = planetMesh.vertices;
		planetMesh.RecalculateNormals ();
		DestroyImmediate(GetComponent<MeshCollider>());
		gameObject.AddComponent(typeof(MeshCollider));
		DestroyImmediate(transform.parent.GetComponent<MeshCollider>());
		transform.parent.gameObject.AddComponent(typeof(MeshCollider));
		transform.parent.gameObject.GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter> ().mesh;
	}

	/*public Vector3[] vertices() {
		List<Vector3> vertexes = new List<Vector3> ();
		foreach (QuadTree q in sides) {
			vertexes.AddRange (q.getSphericalVertices (4, gameObject.transform.position, 1));
		}
		return vertexes.ToArray ();
	}*/

	public Vector3[] expandCube(Vector3[] cubeVertices, float[] heightmap) {
		Vector3[] endVertices = new Vector3[cubeVertices.Length];
		for (int i = 0; i < cubeVertices.Length; i++) {
			endVertices [i] = (cubeVertices [i] - gameObject.transform.position).normalized * (radius + heightmap [i % heightmap.Length]);
		}
		return endVertices;
	}

	public int findIndexOfNearest(Vector3 point) {
		float minMag = float.MaxValue;
		int minIndex = -1;

		for (int i = 0; i < curVertices.Length; i++) {
			float curMag = (transform.TransformPoint(curVertices[i]) - point).magnitude;
			if (curMag < minMag) {
				minMag = curMag;
				minIndex = i;
			}
		}
		return minIndex;
	}

	public float heightAtIndex(int index) {
		return heightMap[index];
	}

	public void setHeightAtIndex(int index, float height) {
		if (editableMap[index]) {
			heightMap[index] = Mathf.Max(Mathf.Min(height, maxHeight), minHeight);
			if (heightMap[index] >= maxHeight || heightMap[index] <= 0) {
				buildableMap[index] = false;
			}
			updateMesh();
		}
	}

	public void incHeightAtIndex(int index, float height) {
		if (editableMap[index]) {
			heightMap[index] = Mathf.Max(Mathf.Min(heightMap[index] + height, maxHeight), minHeight);
			if (heightMap[index] >= maxHeight || heightMap[index] <= 0) {
				buildableMap[index] = false;
			}
			updateMesh();
		}
	}

	public void buildAtIndex(int index, string prefabName) {
		if (buildableMap[index]) {
			GameObject building = Resources.Load("Prefabs/" + prefabName, typeof(GameObject)) as GameObject;
			building = Instantiate(building, transform.TransformPoint(curVertices[index]), Quaternion.identity);
			building.transform.parent = transform.FindChild("Planet Objects");
			building.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
			buildableMap[index] = false;
			editableMap[index] = false;
		}
	}

	public void waterAtIndex(int index) {
		if (heightMap[index] < 0) {
			GameObject water = Resources.Load("Prefabs/Water", typeof(GameObject)) as GameObject;
			water = Instantiate(water, transform.TransformPoint(curVertices[index]) + (transform.TransformPoint(curVertices[index]) - transform.position).normalized * 0.5f, Quaternion.identity);
		}
	}

	public void markAtIndex(int index, string biome) {
		biomeMap[index] = biome;
	}

	public string getBiomeAtIndex(int index) {
		return biomeMap[index];
	}
}
