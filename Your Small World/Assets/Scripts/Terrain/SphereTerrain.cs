using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class SphereTerrain : MonoBehaviour {

	//private List<QuadTree> sides;
	public int radius;

	// Use this for initialization
	void Start () {
		/*sides = new List<QuadTree> ();
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.down, 1, Vector3.right, Vector3.forward));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.up, 1, Vector3.right, Vector3.forward));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.back, 1, Vector3.right, Vector3.up));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.forward, 1, Vector3.right, Vector3.up));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.left, 1, Vector3.forward, Vector3.up));
		sides.Add (new QuadTree (gameObject.transform.position + Vector3.right, 1, Vector3.forward, Vector3.up));*/
		updateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateMesh() {
		Mesh planetMesh = GetComponent<MeshFilter> ().mesh;
		planetMesh.vertices = expandCube (planetMesh.vertices, new float[]{0.1f, 0.2f, 0.3f, -0.1f, -0.2f, 0f, 0.1f});
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
			endVertices [i] = (cubeVertices [i] - gameObject.transform.position) * (radius + heightmap [i % heightmap.Length]);
		}
		return endVertices;
	}
}
