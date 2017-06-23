using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void WaterMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Water")) {
			Debug.Log("Water Desired");
			GetComponent<Community>().SendBoiToGood("Water", v);
		}
	}

	public void StoneMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Stone")) {
			Debug.Log("Stone Desired");
			GetComponent<Community>().SendBoiToGood("Stone", v);
		}
	}

	public void OilMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Oil")) {
			Debug.Log("Oil Desired");
			GetComponent<Community>().SendBoiToGood("Oil", v);
		}
	}

	public void TreeMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Tree")) {
			Debug.Log("Tree Desired");
			GetComponent<Community>().SendBoiToGood("Tree", v);
		}
	}

	public void WheatMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Wheat")) {
			GetComponent<Community>().SendBoiToGood("Wheat", v);
		}
	}

	public void SandMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Sand")) {
			GetComponent<Community>().SendBoiToGood("Sand", v);
		}
	}

	public void IronMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Iron")) {
			GetComponent<Community>().SendBoiToGood("Iron", v);
		}
	}

	public void CopperMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Copper")) {
			GetComponent<Community>().SendBoiToGood("Copper", v);
		}
	}

	public void CoalMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Coal")) {
			GetComponent<Community>().SendBoiToGood("Coal", v);
		}
	}

	public void DeitonMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant("Deiton")) {
			GetComponent<Community>().SendBoiToGood("Deiton", v);
		}
	}
}
