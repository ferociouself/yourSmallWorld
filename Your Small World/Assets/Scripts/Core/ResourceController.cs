using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceController : MonoBehaviour {

	UnityAction action;

	// Use this for initialization
	void Start () {
		action = new UnityAction(WaterMade);
		EventManager.StartListening("Resource", action);
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
}
