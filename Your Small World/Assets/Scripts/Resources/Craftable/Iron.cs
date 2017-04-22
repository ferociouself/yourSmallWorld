using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : CraftableResource {

	public static Iron instance = new Iron();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, IronOre.instance));
		GetPrereqs().Add(new Tuple<int, BaseResource>(2, Smelter.instance));
		SetPrereqNum (2);
		GetCosts().Add(new Tuple<BaseResource, int>(IronOre.instance,2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
