using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree {

	private QuadTreeNode root;

	private Vector3 xAxis, zAxis;

	private float sideLength;

	public QuadTree(Vector3 center, float length, Vector3 x, Vector3 z) {
		this.root = new QuadTreeNode (center, 0);
		this.sideLength = length;
		this.xAxis = x;
		this.zAxis = z;
		this.root.generateVerteces (4, this.sideLength, this.xAxis, this.zAxis);
	}

	public List<Vector3> getSphericalVertices(int depth, Vector3 center, float radius) {
		return this.root.DescendentVertices (depth, center, radius);
	}
}
