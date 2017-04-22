using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : PlaceableResource {

	public static Coal instance = new Coal();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, Tools1.instance));
		SetPrereqNum (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
