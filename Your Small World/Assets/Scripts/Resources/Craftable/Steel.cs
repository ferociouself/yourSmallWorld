using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steel : CraftableResource {

	// Use this for initialization
	void Start () {
		GetPrereqs ().Add (new Tuple<int, BaseResource> (0, new Iron ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, new Coal ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, new Smelter ()));
		SetPrereqNum (3);

		GetCosts ().Add (new Tuple<BaseResource, int> (new Iron (), 1));
		GetCosts ().Add (new Tuple<BaseResource, int> (new Coal (), 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
