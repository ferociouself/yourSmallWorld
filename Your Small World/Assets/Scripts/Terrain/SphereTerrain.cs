using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class SphereTerrain : MonoBehaviour {

	//private List<QuadTree> sides;
	public int radius;
	public Vector3[] curVertices;
	public int[] curTriangles;
	public float[] heightMap;
	public bool[] buildableMap;
	public bool[] editableMap;
	public string[] biomeMap;

	public List<int> buildingIndices;

	public float maxHeight = 0.5f;
	public float minHeight = -0.5f;

	public const string DESERT_BIOME = "Desert";
	public const string LOW_BIOME = "Low";
	public const string MED_BIOME = "Medium";
	public const string HIGH_BIOME = "High";

	// Use this for initialization
	void Start () {
		Mesh planetMesh = GetComponent<MeshFilter> ().mesh;
		heightMap = new float[planetMesh.vertices.Length];
		buildableMap = new bool[planetMesh.vertices.Length];
		biomeMap = new string[planetMesh.vertices.Length];
		buildingIndices = new List<int> ();
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
		curTriangles = planetMesh.triangles;
		planetMesh.RecalculateNormals ();
		DestroyImmediate(GetComponent<MeshCollider>());
		gameObject.AddComponent(typeof(MeshCollider));
		DestroyImmediate(transform.parent.GetComponent<MeshCollider>());
		transform.parent.gameObject.AddComponent(typeof(MeshCollider));
		transform.parent.gameObject.GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter> ().mesh;
	}

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
			buildingIndices.Add (index);
			editableMap[index] = false;
		}
	}

	public void waterAtIndex(int index) {
		if (heightMap[index] < 0) {
			GameObject water = Resources.Load("Prefabs/Water", typeof(GameObject)) as GameObject;
			water = Instantiate(water, transform.TransformPoint(curVertices[index]) + (transform.TransformPoint(curVertices[index]) - transform.position).normalized * 0.5f, Quaternion.identity);
		}
	}
	/*
	 * case BuildType.Water:
					st.waterAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Terrain:
					st.incHeightAtIndex(st.findIndexOfNearest(hitInfo.point), incrDir * 0.1f);
					break;
				case BuildType.Stone:
					// st.StoneAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Sand:
					// st.SandAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Tree:
					// st.TreeAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Wheat:
					// st.WheatAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Oil:
					// st.OilAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Iron:
					// st.IronAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Copper:
					// st.CopperAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Coal:
					// st.CoalAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Deiton:
					// st.DeitonAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				default:
					break;
					*/
	public void StoneAtIndex(int index) {
		if (getBiomeAtIndex(index) == LOW_BIOME) {
			GameObject stone = Resources.Load("Prefabs/Stone", typeof(GameObject)) as GameObject;
			stone = Instantiate(stone, transform.TransformPoint(curVertices[index]), Quaternion.identity);
		}
	}

	public void SandAtIndex(int index) {
		if (getBiomeAtIndex(index) == LOW_BIOME) {
			GameObject stone = Resources.Load("Prefabs/Stone", typeof(GameObject)) as GameObject;
			stone = Instantiate(stone, transform.TransformPoint(curVertices[index]), Quaternion.identity);
		}
	}

	public void markAtIndex(int index, string biome) {
		biomeMap[index] = biome;
	}

	public string getBiomeAtIndex(int index) {
		return biomeMap[index];
	}

	public int[] neighborsOf(Vector3 v) {
		for (int i = 0; i < curVertices.Length; i++) {
			if (curVertices [i] == v) {
				return neighborsOf (i);
			}
		}
		return null;
	}

	public int[] neighborsOf(int index) {
		List<int> neighborIndices = new List<int> (); 
		for (int i = 0; i < curTriangles.Length; i++) {
			if (curTriangles [i] == index) {
				int relativePosition = i % 3;
				switch (relativePosition) {
				case 0:
					if (i + 1 < curTriangles.Length && !neighborIndices.Contains (curTriangles[i + 1])) {
						neighborIndices.Add (curTriangles[i + 1]);
					}
					if (i + 2 < curTriangles.Length && !neighborIndices.Contains (curTriangles[i + 2])) {
						neighborIndices.Add (curTriangles[i + 2]);
					}
					break;
				case 1:
					if (!neighborIndices.Contains (curTriangles[i - 1])) {
						neighborIndices.Add (curTriangles[i - 1]);
					}
					if (i + 1 < curTriangles.Length && !neighborIndices.Contains (curTriangles[i + 1])) {
						neighborIndices.Add (curTriangles[i + 1]);
					}
					break;
				case 2:
					if (!neighborIndices.Contains (curTriangles[i - 1])) {
						neighborIndices.Add (curTriangles[i - 1]);
					}
					if (!neighborIndices.Contains (curTriangles[i - 2])) {
						neighborIndices.Add (curTriangles[i - 2]);
					}
					break;
				}
			}
		}
		return neighborIndices.ToArray ();
	}

	public int[] currentBuildings() {
		return buildingIndices.ToArray ();
	}
}
