using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copper : CraftableResource {

	public static Copper instance = new Copper();

	// Use this for initialization
	void Start () {
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, new CopperOre ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, new Smelter ()));
		SetPrereqNum (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
