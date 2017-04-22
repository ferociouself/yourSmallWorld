using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : PlaceableResource {

	public static Marble instance = new Marble();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new Tools2()));
		SetPrereqNum (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
