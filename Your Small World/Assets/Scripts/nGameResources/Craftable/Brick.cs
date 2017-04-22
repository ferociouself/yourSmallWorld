using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : CraftableResource {

	public static Brick instance = new Brick();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, Clay.instance));
		GetPrereqs().Add(new Tuple<int, BaseResource>(2, Fire.instance));
		SetPrereqNum (2);
		GetCosts().Add(new Tuple<BaseResource, int>(Clay.instance, 1));
		GetCosts().Add(new Tuple<BaseResource, int>(Fire.instance, 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
