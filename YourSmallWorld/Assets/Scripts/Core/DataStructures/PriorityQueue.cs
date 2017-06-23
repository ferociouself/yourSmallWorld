using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue {
	private ArrayList nodes = new ArrayList();
	private List<Vertex> vertices = new List<Vertex>();

	public int Length {
		get { return this.nodes.Count; }
	}

	public bool Contains(Node node) {
		return this.vertices.Contains(node.vert);
	}

	public Node First() {
		if (this.nodes.Count > 0) {
			return (Node)this.nodes[0];
		}
		return null;
	}

	public void Push(Node node) {
		this.nodes.Add(node);
		this.vertices.Add(node.vert);
		this.nodes.Sort();
	}

	public void Remove(Node node) {
		this.nodes.Remove(node);
		this.vertices.Remove(node.vert);
		this.nodes.Sort();
	}
}
