using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : BaseBuilding {

	public static Smelter instance = new Smelter();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, IronOre.instance));
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, CopperOre.instance));
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, GoldOre.instance));
		GetPrereqs().Add(new Tuple<int, BaseResource>(2, Fire.instance));
		SetPrereqNum (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
