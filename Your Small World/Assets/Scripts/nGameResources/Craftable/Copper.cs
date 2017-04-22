using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copper : CraftableResource {

	public static Copper instance = new Copper();

	// Use this for initialization
	void Start () {
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, CopperOre.instance ));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, Smelter.instance ));
		SetPrereqNum (2);
		GetCosts().Add(new Tuple<BaseResource, int>(CopperOre.instance,2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
