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

	public void ResourceMade(BaseResource b, Vertex v) {
		Debug.Log("Resource made");
		if (GetComponent<TierController>().CheckIfWant(b)) {
			GetComponent<Community>().SendBoiToGood(b, v);
		}
	}

	void WaterMade() {
		Debug.Log("Water Made");
	}

	public void WaterMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant(Water.instance)) {
			GetComponent<Community>().SendBoiToGood(Water.instance, v);
		}
	}

	public void StoneMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant(Stone.instance)) {
			GetComponent<Community>().SendBoiToGood(Stone.instance, v);
		}
	}

	public void OilMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant(Oil.instance)) {
			GetComponent<Community>().SendBoiToGood(Oil.instance, v);
		}
	}

	public void TreeMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant(Tree.instance)) {
			GetComponent<Community>().SendBoiToGood(Tree.instance, v);
		}
	}

	public void WheatMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant(Wheat.instance)) {
			GetComponent<Community>().SendBoiToGood(Wheat.instance, v);
		}
	}

	public void SandMade(Vertex v) {
		if (GetComponent<TierController>().CheckIfWant(Sand.instance)) {
			GetComponent<Community>().SendBoiToGood(Sand.instance, v);
		}
	}
}
