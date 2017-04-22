using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : BaseBuilding {

	public static Smelter instance = new Smelter();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new IronOre()));
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new CopperOre()));
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new GoldOre()));
		GetPrereqs().Add(new Tuple<int, BaseResource>(2, new Fire()));
		SetPrereqNum (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
