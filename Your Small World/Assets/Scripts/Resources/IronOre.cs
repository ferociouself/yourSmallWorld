using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronOre : PlaceableResource {

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new Tools1()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
