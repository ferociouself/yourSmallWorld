using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Node : IComparable {

	public Vertex vert;
	public float nodeTotalCost, estimatedCost;
	public Node parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int CompareTo(object n2) {
		if (n2 is Node) {
			if (this.estimatedCost < ((Node)n2).estimatedCost) {
				return -1;
			}
			if (this.estimatedCost > ((Node)n2).estimatedCost) {
				return 1;
			}
			return 0;
		} else {
			return -1;
		}
	}
}
