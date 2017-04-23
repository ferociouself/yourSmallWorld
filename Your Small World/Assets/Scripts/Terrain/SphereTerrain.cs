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
	public bool[] editableMap;
	public string[] biomeMap;
	public GameObject[] resourceMap;

	public List<int> buildingIndices;

	public float maxHeight = 0.5f;
	public float minHeight = -0.5f;

	public const string DESERT_BIOME = "Desert";
	public const string LOW_BIOME = "Low";
	public const string MED_BIOME = "Medium";
	public const string HIGH_BIOME = "High";
	public const string WATER_BIOME = "Water";
	public const string OIL_BIOME = "Oil";
	public const string STONE_BIOME = "Stone";



	List<Color> blues;
	List<Color> greens;
	List<Color> yellows;
	List<Color> greays;
	List<Color> whites;
	List<Color> browns;
	List<Color> stones;
	List<Color> oils;

	MeshFilter filter;

	// Use this for initialization
	void Start () {
		filter = GetComponent<MeshFilter> ();
		Mesh planetMesh = filter.mesh;
		heightMap = new float[planetMesh.vertices.Length];
		biomeMap = new string[planetMesh.vertices.Length];
		buildingIndices = new List<int> ();
		editableMap = new bool[planetMesh.vertices.Length];
		for (int i = 0; i < planetMesh.vertices.Length; i++) {
			heightMap[i] = 0.0f;
			editableMap[i] = true;
			biomeMap[i] = "Desert";
		}


		blues = new List<Color> ();
		blues.Add(lazyColor(15,94,156));
		blues.Add(lazyColor(35,137,218));
		blues.Add(lazyColor(28,163,236));
		blues.Add(lazyColor(90,188,216));
		blues.Add(lazyColor(116,204,244));

		greens = new List<Color> ();
		greens.Add(lazyColor(79,141,47));
		greens.Add(lazyColor(58,124,37));
		greens.Add(lazyColor(19,99,33));
		greens.Add(lazyColor(22,99,54));
		greens.Add(lazyColor(8,88,58));

		yellows = new List<Color> ();
		yellows.Add(lazyColor(236,216,98));
		yellows.Add(lazyColor(242,203,80));
		//yellows.Add(lazyColor(221,171,51));

		greays = new List<Color> ();
		greays.Add(lazyColor(204,216,218));
		greays.Add(lazyColor(143,152,171));
		greays.Add(lazyColor(120,126,149));
		greays.Add(lazyColor(120,126,149));
		greays.Add(lazyColor(120,126,149));

		whites = new List<Color> ();
		whites.Add(lazyColor(235,252,255));
		whites.Add(lazyColor(224,252,255));
		whites.Add(lazyColor(208,255,255));
		whites.Add(lazyColor(204,251,255));
		whites.Add(lazyColor(188,255,255));

		browns = new List<Color> ();
		browns.Add(lazyColor(41,24,10));
		browns.Add(lazyColor(45,26,12));
		browns.Add(lazyColor(60,34,16));
		browns.Add(lazyColor(71,40,18));
		browns.Add(lazyColor(84,48,22));

		stones = new List<Color> ();
		stones.Add(lazyColor(199,203,210));
		stones.Add(lazyColor(196,196,196));
		stones.Add(lazyColor(186,195,201));
		stones.Add(lazyColor(152,160,167));

		oils = new List<Color> ();
		oils.Add (lazyColor (43,4,4));
		oils.Add (lazyColor (43,26,4));
		oils.Add (lazyColor (11,43,4));
		oils.Add (lazyColor (4,11,43));
		oils.Add (lazyColor (31,4,43));

		updateMesh ();
		rebuildColors ();
	}

	private Color lazyColor(int r, int g, int b){
		return new Color(((float)r)/255.0f, ((float)g)/255.0f, ((float)b)/255.0f);
	}
	
	// Update is called once per frame
	void Update () {
		List<int> waterVertices = new List<int> ();
		for (int i = 0; i < biomeMap.Length; i++) {
			if (biomeMap [i] == WATER_BIOME) {
				waterVertices.Add (i);
			}
		}
		foreach (int k in waterVertices) {
			int[] neighbors = neighborsOf (k);
			for(int l = 0; l < neighbors.Length; l++) {
				if (biomeMap [neighbors [l]] != WATER_BIOME && heightMap [neighbors [l]] == 0) {
					if (Random.Range (0, 500) < 1) {
						markAtIndex (neighbors [l], MED_BIOME);
					}
				}
			}
		}
	}

	public void updateMesh() {
		Mesh planetMesh = filter.mesh;
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
			if (heightMap[index] > 0.0f) {
				markAtIndex(index, HIGH_BIOME);
			} else if (heightMap[index] < 0.0f) {
				markAtIndex(index, LOW_BIOME);
				int[] neighbors = neighborsOf (index);
				bool nextToWater = false;
				for (int i = 0; i < neighbors.Length; i++) {
					if (biomeMap [neighbors [i]] == WATER_BIOME) {
						nextToWater = true;
					}
				}
				if (nextToWater) {
					spreadWaterBiome (index);
				}
			} else {
				if (biomeMap[index] != MED_BIOME) {
					markAtIndex(index, DESERT_BIOME);
				}
			}
			updateMesh();
		}
	}

	public void incHeightAtIndex(int index, float height) {
		if (editableMap[index]) {
			heightMap[index] = Mathf.Max(Mathf.Min(heightMap[index] + height, maxHeight), minHeight);
			if (heightMap[index] > 0.0f) {
				markAtIndex(index, HIGH_BIOME);
			} else if (heightMap[index] < 0.0f) {
				markAtIndex(index, LOW_BIOME);
				int[] neighbors = neighborsOf (index);
				bool nextToWater = false;
				for (int i = 0; i < neighbors.Length; i++) {
					if (biomeMap [neighbors [i]] == WATER_BIOME) {
						nextToWater = true;
					}
				}
				if (nextToWater) {
					spreadWaterBiome (index);
				}
			} else {
				if (biomeMap[index] != MED_BIOME) {
					markAtIndex(index, DESERT_BIOME);
				}
			}
			updateMesh();
		}
	}

	public void buildAtIndex(int index, string prefabName) {
		if (editableMap[index]) {
			GameObject building = Resources.Load("Prefabs/" + prefabName, typeof(GameObject)) as GameObject;
			building = Instantiate(building, transform.TransformPoint(curVertices[index]), Quaternion.identity);
			building.transform.parent = transform.FindChild("Planet Objects");
			building.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
			buildingIndices.Add (index);
			resourceMap[index] = building;
			editableMap[index] = false;
		}
	}

	public void waterAtIndex(int index) {
		if (heightMap[index] < 0) {
			//GameObject water = Resources.Load("Prefabs/Water", typeof(GameObject)) as GameObject;
			//water = Instantiate(water, transform.TransformPoint(curVertices[index]) + (transform.TransformPoint(curVertices[index]) - transform.position).normalized * 0.5f, Quaternion.identity);
			spreadWaterBiome (index);
		}
	}

	public void spreadWaterBiome(int index) {
		markAtIndex (index, WATER_BIOME);
		int[] neighbors = neighborsOf (index);
		Debug.Log (neighbors.Length);
		for (int i = 0; i < neighbors.Length; i++) {
			Debug.Log (heightMap [neighbors[1]]);
			if (heightMap [neighbors[i]] < 0 && getBiomeAtIndex(neighbors[i]) != WATER_BIOME) {
				spreadWaterBiome (neighbors[i]);
			}
		}
	}

	public void StoneAtIndex(int index) {
		if (getBiomeAtIndex(index) == LOW_BIOME) {
			markAtIndex(index, STONE_BIOME);
		}
	}

	public void SandAtIndex(int index) {
		if (getBiomeAtIndex(index) == MED_BIOME) {
			GameObject sand = Resources.Load("Prefabs/Sand", typeof(GameObject)) as GameObject;
			sand = Instantiate(sand, transform.TransformPoint(curVertices[index]), Quaternion.identity) as GameObject;
			resourceMap[index] = sand;
		}
	}

	public void TreeAtIndex(int index) {
		if (getBiomeAtIndex(index) == MED_BIOME) {
			GameObject tree = Resources.Load("Prefabs/Tree", typeof(GameObject)) as GameObject;
			tree = Instantiate(tree, transform.TransformPoint(curVertices[index]), Quaternion.identity) as GameObject;
			resourceMap[index] = tree;
		}
	}

	public void WheatAtIndex(int index) {
		if (getBiomeAtIndex(index) == MED_BIOME) {
			GameObject wheat = Resources.Load("Prefabs/Wheat", typeof(GameObject)) as GameObject;
			wheat = Instantiate(wheat, transform.TransformPoint(curVertices[index]), Quaternion.identity) as GameObject;
			resourceMap[index] = wheat;
		}
	}

	public void OilAtIndex(int index) {
		if (getBiomeAtIndex(index) == LOW_BIOME) {
			markAtIndex(index, OIL_BIOME);
		}
	}

	public void IronAtIndex(int index) {
		if (getBiomeAtIndex(index) == HIGH_BIOME) {
			GameObject iron = Resources.Load("Prefabs/Iron", typeof(GameObject)) as GameObject;
			iron = Instantiate(iron, transform.TransformPoint(curVertices[index]), Quaternion.identity) as GameObject;
			resourceMap[index] = iron;
		}
	}

	public void CopperAtIndex(int index) {
		if (getBiomeAtIndex(index) == HIGH_BIOME) {
			GameObject copper = Resources.Load("Prefabs/Copper", typeof(GameObject)) as GameObject;
			copper = Instantiate(copper, transform.TransformPoint(curVertices[index]), Quaternion.identity) as GameObject;
			resourceMap[index] = copper;
		}
	}

	public void CoalAtIndex(int index) {
		if (getBiomeAtIndex(index) == HIGH_BIOME) {
			GameObject coal = Resources.Load("Prefabs/Coal", typeof(GameObject)) as GameObject;
			coal = Instantiate(coal, transform.TransformPoint(curVertices[index]), Quaternion.identity) as GameObject;
			resourceMap[index] = coal;
		}
	}

	public void DeitonAtIndex(int index) {
		if (getBiomeAtIndex(index) == HIGH_BIOME) {
			GameObject deiton = Resources.Load("Prefabs/Deiton", typeof(GameObject)) as GameObject;
			deiton = Instantiate(deiton, transform.TransformPoint(curVertices[index]) + (transform.TransformPoint(curVertices[index]) - transform.position).normalized * 0.5f, Quaternion.identity) as GameObject;
			resourceMap[index] = deiton;
		}
	}

	public void markAtIndex(int index, string biome) {
		if (biomeMap [index] == biome) {
			return;
		}
		biomeMap[index] = biome;
		Color[] colors = filter.mesh.colors;
		if (colors.Length != filter.mesh.vertices.Length) {
			rebuildColors ();
		}
		colors = filter.mesh.colors;
		switch (biome) {
		case WATER_BIOME:
			colors [index] = blues[Random.Range(0, blues.Count)];
			break;
		case DESERT_BIOME:
			colors [index] = yellows[Random.Range(0, yellows.Count)];
			break;
		case HIGH_BIOME:
			colors [index] = greays[Random.Range(0, greays.Count)];
			break;
		case MED_BIOME:
			colors [index] = greens[Random.Range(0, greens.Count)];
			break;
		case LOW_BIOME:
			colors [index] = browns[Random.Range(0, browns.Count)];
			break;
		case STONE_BIOME:
			colors [index] = stones [Random.Range (0, stones.Count)];
			break;
		case OIL_BIOME:
			colors [index] = oils [Random.Range (0, oils.Count)];
			break;
		default:
			colors [index] = yellows[Random.Range(0, yellows.Count)];
			break;
		}
		filter.mesh.colors = colors;
	}

	public void rebuildColors() {
		Debug.Log ("rebuilding....");
		Color[] c = new Color[filter.mesh.vertices.Length];
		for (int i = 0; i < filter.mesh.vertices.Length; i++) {
			switch (getBiomeAtIndex(i)) {
			case WATER_BIOME:
				c [i] = blues[Random.Range(0, blues.Count)];
				break;
			case DESERT_BIOME:
				c [i] = yellows[Random.Range(0, yellows.Count)];
				break;
			case HIGH_BIOME:
				c [i] = greays[Random.Range(0, greays.Count)];
				break;
			case MED_BIOME:
				c [i] = greens[Random.Range(0, greens.Count)];
				break;
			case LOW_BIOME:
				c [i] = browns[Random.Range(0, browns.Count)];
				break;
			case STONE_BIOME:
				c [i] = greays [Random.Range (0, greays.Count)];
				break;
			case OIL_BIOME:
				c [i] = oils [Random.Range (0, oils.Count)];
				break;
			default:
				c [i] = yellows[Random.Range(0, yellows.Count)];
				break;
			}
		}
		filter.mesh.colors = c;
		filter.mesh.RecalculateBounds ();
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
	