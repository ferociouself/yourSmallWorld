using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : CraftableResource {

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new GoldOre()));
		GetPrereqs().Add(new Tuple<int, BaseResource>(2, new Smelter()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
