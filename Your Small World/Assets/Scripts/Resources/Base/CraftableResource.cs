using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableResource : BaseResource {

	private List<Tuple<BaseResource, int>> costs;

	// Use this for initialization
	void Start () {
		costs = new List<Tuple<BaseResource, int>> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public List<Tuple<BaseResource, int>> GetCosts(){
		return costs;
	}
}
