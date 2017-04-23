using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {

	public static PriorityQueue closedList, openList;

	//public static Queue<Node> closedList, openList;

	private static float HeuristicEstimateCost(Vertex curVert, Vertex goalVert) {
		Vector3 vecCost = curVert.getTransformedPoint() - goalVert.getTransformedPoint();
		return vecCost.magnitude;
	}

	public static List<Vertex> FindPath(Vertex start, Vertex goal, GameObject man) {
		openList = new PriorityQueue();
		Node startNode = new Node();
		startNode.nodeTotalCost = 0.0f;
		startNode.estimatedCost = HeuristicEstimateCost(start, goal);
		startNode.vert = start;
		openList.Push(startNode);
		//openList.Enqueue(startNode);

		closedList = new PriorityQueue();

		Node curNode = null;

		int iterationCount = 0;
		int maxIterationCount = 1000;

		while (openList.Length > 0 && iterationCount < maxIterationCount) {
			curNode = openList.First();

			if (curNode.vert == goal) {
				return CalculatePath(curNode);
			}

			List<Node> neighbors = new List<Node>();

			/*foreach (Vertex v in curNode.vert.getNeighbors()) {
				if (v.getTransversable()) {
					Node newNode = new Node();
					newNode.vert = v;
					newNode.estimatedCost = 0.0f;
					newNode.nodeTotalCost = 0.0f;
					neighbors.Add(newNode);
				}
			}*/

			Vertex[] neighborVertices = curNode.vert.getNeighbors();
			for (int i = 0; i < neighborVertices.Length; i++) {
				Vertex v = neighborVertices[i];
				if (v.getTransversable()) {
					Node newNode = new Node();
					newNode.vert = v;
					newNode.estimatedCost = HeuristicEstimateCost(v, goal);
					newNode.nodeTotalCost = 0.0f;
					neighbors.Add(newNode);
				}
			}

			for (int i = 0; i < neighbors.Count; i++) {
				Node neighborNode = neighbors[i];

				if (!closedList.Contains(neighborNode)) {
					float cost = 0.0f;
					if (neighborNode.vert.getTransversable()) {
						cost = HeuristicEstimateCost(curNode.vert, neighborNode.vert);
					} else {
						cost = 10000.0f;
					}

					float totalCost = curNode.nodeTotalCost + cost;
					float neighborNodeEstCost = HeuristicEstimateCost(neighborNode.vert, goal);

					neighborNode.nodeTotalCost = totalCost;
					neighborNode.estimatedCost = neighborNodeEstCost;
					neighborNode.parent = curNode;

					if (!openList.Contains(neighborNode)) {
						openList.Push(neighborNode);
					}
				}
			}

			closedList.Push(curNode);
			openList.Remove(curNode);
			iterationCount++;
		}

		if (curNode.vert != goal) {
			Debug.LogError("Goal Not Found!");
			return null;
		}
		return CalculatePath(curNode);
	}

	private static List<Vertex> CalculatePath(Node node) {
		List<Vertex> list = new List<Vertex>();
		while (node != null) {
			list.Add(node.vert);
			node = node.parent;
		}
		list.Reverse();
		return list;
	}
}
