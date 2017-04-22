using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode {

	//Location on cube
	private Vector3 location;

	//height of terrain
	private float height;

	//...... a child
	private List<QuadTreeNode> children;

	//Constructors
	public QuadTreeNode() {
		this.location = new Vector3 (0, 0, 0);
		this.height = 0;
		this.children = new List<QuadTreeNode>();
	}

	public QuadTreeNode(Vector3 pos) {
		this.location = pos;
		this.height = 0;
		this.children = new List<QuadTreeNode>();
	}

	public QuadTreeNode(Vector3 pos, float h) {
		this.location = pos;
		this.height = h;
		this.children = new List<QuadTreeNode>();
	}

	public QuadTreeNode(Vector3 pos, float h, List<QuadTreeNode> c) {
		this.location = pos;
		this.height = h;
		this.children = c;
	}

	/// <summary>
	/// Returns a list of all the spherical vertices of all the children of this node. If this is a pseudo- or real leaf node, return a list just containing this point's vertex.
	/// Is a pseudo-leaf if @depth == 0
	/// Sphere is assumed to be centered at the vector @center
	/// </summary>
	/// <returns>The vertices.</returns>
	/// <param name="n">Number of layers to continue down</param>
	/// <param name="center">Center of the sphere</param>
	/// <param name="radius">Radius of the sphere</param> 
	public List<Vector3> DescendentVertices(int depth, Vector3 center, float radius) {
		List<Vector3> vectors = new List<Vector3> ();
		if (depth <= 0) {
			vectors.Add (this.toSphericalVector (center, radius));
			return vectors;
		}
		if (this.children.Count == 0) { 
			vectors.Add (this.toSphericalVector (center, radius));
			return vectors;
		}
		foreach (QuadTreeNode q in children) {
			vectors.AddRange (q.DescendentVertices (depth - 1, center, radius)); //Adds next layer down for each child
		}
		return vectors;
	}

	/// <summary>
	/// Converts this point to a vertex of the sphere
	/// </summary>
	/// <returns>The spherical vector.</returns>
	/// <param name="center">Center of the sphere.</param>
	/// <param name="radius">Radius of the sphere.</param>
	public Vector3 toSphericalVector(Vector3 center, float radius) {
		Vector3 radial = this.location - center;
		return radial.normalized * (radius + this.getHeight());
	}

	/// <summary>
	/// Calculate the height of this vertex
	/// </summary>
	/// <returns>The height of this vertex. Recalculates as the average of children, if this node has any.</returns>
	public float getHeight() {
		if (this.children.Count != 0) {
			float sum = 0;
			foreach (QuadTreeNode q in children) {
				sum += q.getHeight ();
			}
			this.height = sum / this.children.Count;
		}
		return this.height;
	}

	/// <summary>
	/// Generates a plane of vertices
	/// Will kill all existing children.
	/// </summary>
	/// <param name="depth">Depth of sub-tree to make</param>
	/// <param name="length">Length of the square this vertex represents</param>
	/// <param name="xAxis">X axis.</param>
	/// <param name="zAxis">Z axis.</param>
	public void generateVerteces(int depth, float length, Vector3 xAxis, Vector3 zAxis) {
		//kill the children. do it.
		this.children = new List<QuadTreeNode>();

		//Normalize basis vectors
		xAxis = xAxis.normalized;
		zAxis = zAxis.normalized;

		if (depth > 0) {
			//Upper left
			this.children.Add (new QuadTreeNode (this.location + (length / 2) * (zAxis - xAxis), this.height));
			//Upper right
			this.children.Add (new QuadTreeNode (this.location + (length / 2) * (zAxis + xAxis), this.height));
			//Lower Left
			this.children.Add (new QuadTreeNode (this.location + -1 * (length / 2) * (zAxis - xAxis), this.height));
			//Lower Right
			this.children.Add (new QuadTreeNode (this.location + -1 * (length / 2) * (zAxis + xAxis), this.height));
			//RECURSE
			foreach (QuadTreeNode q in children) {
				q.generateVerteces (depth - 1, length / 2, xAxis, zAxis);
			}
		}
	}
}
