using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class SphereTerrain : MonoBehaviour {

	public int radius;
	public Vertex[] vertices;
	public int[] curTriangles;

	/*public Vector3[] curVertices;
	public float[] heightMap;
	public bool[] editableMap;
	public string[] biomeMap;
	public GameObject[] resourceMap;*/

	public List<Vertex> buildingIndices;

	public float maxHeight = 0.5f;
	public float minHeight = -0.5f;

	public const string DESERT_BIOME = "Desert";
	public const string LOW_BIOME = "Low";
	public const string MED_BIOME = "Medium";
	public const string HIGH_BIOME = "High";
	public const string WATER_BIOME = "Water";
	public const string OIL_BIOME = "Oil";
	public const string STONE_BIOME = "Stone";



	public static List<Color> blues;
	public static List<Color> greens;
	public static List<Color> yellows;
	public static List<Color> greays;
	public static List<Color> whites;
	public static List<Color> browns;
	public static List<Color> stones;
	public static List<Color> oils;

	public MeshFilter filter;

	// Use this for initialization
	void Start () {
		filter = GetComponent<MeshFilter> ();
		Mesh planetMesh = filter.mesh;

		blues = new List<Color> ();
		blues.Add(lazyColor(15,94,156));
		blues.Add(lazyColor(35,137,218));
		blues.Add(lazyColor(28,163,236));
		blues.Add(lazyColor(90,188,216));
		//blues.Add(lazyColor(116,204,244));

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

		vertices = new Vertex[planetMesh.vertices.Length];

		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] = new Vertex (i, planetMesh.vertices [i], this);
		}

		buildingIndices = new List<Vertex> ();

		generateNeighborFieldsAsync ();

		updateMesh ();
		updateColors ();
	}

	private Color lazyColor(int r, int g, int b){
		return new Color(((float)r)/255.0f, ((float)g)/255.0f, ((float)b)/255.0f);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < vertices.Length; i++) {
			if (vertices [i].getBiome () == WATER_BIOME) {
				Vertex[] neighbors = vertices [i].getNeighbors ();
				for (int l = 0; l < neighbors.Length; l++) {
					if (neighbors [l].getBiome () != WATER_BIOME && neighbors [l].getHeight () == 0) {
						if (Random.Range (0, 500) < 1) {
							neighbors [l].setBiome (MED_BIOME);
						}
					}
				}
			} else if (vertices [i].getBiome () == MED_BIOME) {
				Vertex[] neighbors = vertices [i].getNeighbors ();
				bool hasWaterNeighbor = false;
				for (int l = 0; l < neighbors.Length; l++) {
					if (neighbors [l].getBiome () == WATER_BIOME) {
						hasWaterNeighbor = true;
					}
				}
				if (hasWaterNeighbor) {
					for (int k = 0; k < neighbors.Length; k++) {
						if (neighbors [k].getBiome () != WATER_BIOME && neighbors [k].getHeight () == 0) {
							if (Random.Range (0, 10000) < 1) {
								neighbors [k].setBiome (MED_BIOME);
							}
						}
					}
				}
			}
		}
	}

	public void updateMesh() {
		Mesh planetMesh = filter.mesh;
		planetMesh.vertices = generateMesh ();
		curTriangles = planetMesh.triangles;
		planetMesh.RecalculateNormals ();
		DestroyImmediate(GetComponent<MeshCollider>());
		gameObject.AddComponent(typeof(MeshCollider));
		DestroyImmediate(transform.parent.GetComponent<MeshCollider>());
		transform.parent.gameObject.AddComponent(typeof(MeshCollider));
		transform.parent.gameObject.GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter> ().mesh;
	}

	public Vector3[] generateMesh() {
		Vector3[] points = new Vector3[vertices.Length];
		for(int i = 0; i < points.Length; i++) {
			points [i] = vertices [i].getSphereVector (this.radius, this.gameObject.transform.position);
		}
		return points;
	}

	public int findIndexOfNearest(Vector3 point) {
		float minMag = float.MaxValue;
		int minIndex = -1;

		for (int i = 0; i < vertices.Length; i++) {
			float curMag = (transform.TransformPoint(vertices[i].getSphereVector()) - point).magnitude;
			if (curMag < minMag) {
				minMag = curMag;
				minIndex = i;
			}
		}
		return minIndex;
	}

	public float heightAtIndex(int index) {
		return vertices[index].getHeight();
	}

	public void setHeightAtIndex(int index, float height) {
		vertices [index].setHeight (height);
		updateMesh ();
	}

	public void incHeightAtIndex(int index, float height) {
		setHeightAtIndex (index, vertices [index].getHeight () + height);
	}

	public void buildAtIndex(int index, string prefabName) {
		if (vertices[index].getIsEditable()) {
			vertices [index].removeResource ();
			GameObject building = Resources.Load("Prefabs/" + prefabName, typeof(GameObject)) as GameObject;
			building = Instantiate(building, transform.TransformPoint(vertices[index].getSphereVector(this.radius, this.gameObject.transform.position)), Quaternion.identity);
			building.transform.parent = transform.FindChild("Planet Objects");
			building.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
			buildingIndices.Add (vertices[index]);
			vertices [index].setIsEditable (false);
		}
	}

	public void waterAtIndex(int index) {
		if (vertices[index].getHeight() < 0) {
			spreadWaterBiome (vertices[index]);
		}
	}

	public void spreadWaterBiome(Vertex v) {
		if (v.getBiome() == LOW_BIOME || v.getBiome() == OIL_BIOME || v.getBiome() == STONE_BIOME) {
			v.setBiome (WATER_BIOME);
			Vertex[] neighbors = v.getNeighbors ();
			for (int i = 0; i < neighbors.Length; i++) {
				if (neighbors[i].getHeight() < 0 && neighbors[i].getBiome() != WATER_BIOME) {
					spreadWaterBiome (neighbors[i]);
				}
			}
		}
	}

	public void StoneAtIndex(int index) {
		SpreadStoneBiome (vertices[index]);
	}

	public void SpreadStoneBiome(Vertex v) {
		if (v.getBiome() == LOW_BIOME || v.getBiome() == OIL_BIOME || v.getBiome() == WATER_BIOME) {
			v.setBiome (STONE_BIOME);
			GameObject stone = Resources.Load("Prefabs/Stone" + Random.Range(0,1), typeof(GameObject)) as GameObject;
			stone = Instantiate(stone, transform.TransformPoint(v.getSphereVector()), Quaternion.identity) as GameObject;
			v.removeResource ();
			v.setResource (stone);
			Vertex[] neighbors = v.getNeighbors ();
			for (int i = 0; i < neighbors.Length; i++) {
				if (neighbors[i].getHeight() < 0 && neighbors[i].getBiome() != STONE_BIOME) {
					SpreadStoneBiome (neighbors[i]);
				}
			}
		}
	}

	public void SandAtIndex(int index) {
		if (vertices[index].getBiome() == DESERT_BIOME || vertices[index].getBiome() == MED_BIOME) {
			if(vertices[index].getResource() != null && vertices[index].getResource().name.Contains("SandHills")) {
				return;
			}
			GameObject sand = Resources.Load("Prefabs/SandHill" + Random.Range(0,2), typeof(GameObject)) as GameObject;
			sand = Instantiate(sand, transform.TransformPoint(vertices[index].getSphereVector()), Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (sand);
		}
	}

	public void TreeAtIndex(int index) {
		if (vertices[index].getBiome() == MED_BIOME) {
			if (vertices [index].getResource () != null && vertices [index].getResource ().name.Contains ("Forest")) {
				return;
			}
			GameObject tree = Resources.Load("Prefabs/Forest" + Random.Range(0,5), typeof(GameObject)) as GameObject;
			float randRot = Random.Range(0.0f, 180.0f);
			tree.transform.GetChild (0).transform.rotation = Quaternion.Euler (0.0f, randRot, 0.0f);
			tree = Instantiate(tree, transform.TransformPoint(vertices[index].getSphereVector()), Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (tree);
		}
	}

	public void WheatAtIndex(int index) {
		if (vertices[index].getBiome() == MED_BIOME) {
			if (vertices [index].getResource () != null && vertices [index].getResource ().name.Contains ("WheatField")) {
				return;
			}
			GameObject wheat = Resources.Load("Prefabs/WheatField0", typeof(GameObject)) as GameObject;
			float randRot = 0.0f;//Random.Range(0.0f, 180.0f);
			wheat.transform.GetChild (0).transform.rotation = Quaternion.Euler (0.0f, randRot, 0.0f);
			wheat = Instantiate(wheat, transform.TransformPoint(vertices[index].getSphereVector()), Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (wheat);
		}
	}

	public void OilAtIndex(int index) {
		SpreadOilBiome (vertices[index]);
	}

	public void SpreadOilBiome(Vertex v) {
		if (v.getBiome() == LOW_BIOME || v.getBiome() == WATER_BIOME || v.getBiome() == STONE_BIOME) {
			v.setBiome (OIL_BIOME);
			Vertex[] neighbors = v.getNeighbors ();
			for (int i = 0; i < neighbors.Length; i++) {
				if (neighbors[i].getHeight() < 0 && neighbors[i].getBiome() != OIL_BIOME) {
					SpreadOilBiome (neighbors[i]);
				}
			}
		}
	}

	public void IronAtIndex(int index) {
		if (vertices[index].getBiome() == HIGH_BIOME) {
			if (vertices [index].getResource () != null && vertices [index].getResource ().name.Contains ("Iron")) {
				return;
			}
			GameObject iron = Resources.Load("Prefabs/IronVein" + Random.Range(0,1), typeof(GameObject)) as GameObject;
			iron = Instantiate(iron, transform.TransformPoint(vertices[index].getSphereVector()), Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (iron);
		}
	}

	public void CopperAtIndex(int index) {
		if (getBiomeAtIndex(index) == HIGH_BIOME) {
			if (vertices [index].getResource () != null && vertices [index].getResource ().name.Contains ("Copper")) {
				return;
			}
			GameObject copper = Resources.Load("Prefabs/CopperVein" + Random.Range(0,1), typeof(GameObject)) as GameObject;
			copper = Instantiate(copper, transform.TransformPoint(vertices[index].getSphereVector()), Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (copper);
		}
	}

	public void CoalAtIndex(int index) {
		if (getBiomeAtIndex(index) == HIGH_BIOME) {
			if (vertices [index].getResource () != null && vertices [index].getResource ().name.Contains ("Coal")) {
				return;
			}
			GameObject coal = Resources.Load("Prefabs/CoalVein" + Random.Range(0,1), typeof(GameObject)) as GameObject;
			coal = Instantiate(coal, transform.TransformPoint(vertices[index].getSphereVector()), Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (coal);
		}
	}

	public void DeitonAtIndex(int index) {
		if (vertices[index].getBiome() == HIGH_BIOME) {
			if (vertices [index].getResource () != null && vertices [index].getResource ().name.Contains ("Deiton")) {
				return;
			}
			GameObject deiton = Resources.Load("Prefabs/Deiton", typeof(GameObject)) as GameObject;
			deiton = Instantiate(deiton, transform.TransformPoint(vertices[index].getSphereVector()) + (transform.TransformPoint(vertices[index].getSphereVector()) - transform.position).normalized * 0.5f, Quaternion.identity) as GameObject;
			vertices [index].removeResource ();
			vertices [index].setResource (deiton);
		}
	}

	public void markAtIndex(int index, string biome) {
		vertices [index].setBiome (biome);
	}

	public void updateColors() {
		Color[] colors = new Color[vertices.Length];
		for (int i = 0; i < vertices.Length; i++) {
			colors [i] = vertices [i].getColor ();
		}
		filter.mesh.colors = colors;
	}

	public void rebuildColors() {
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
		return vertices[index].getBiome();
	}

	public Vertex[] currentBuildings() {
		return buildingIndices.ToArray ();
	}

	public Vertex getVertex(int i) {
		return vertices [i];
	}

	public IEnumerator generateNeighborFieldsAsync() {
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i].calculateNeighbors ();
			yield return null;
		}
	}
}
	