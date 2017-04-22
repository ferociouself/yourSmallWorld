using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldOre : PlaceableResource {

	public static GoldOre instance = new GoldOre();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, Tools1.instance));
		SetPrereqNum (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
