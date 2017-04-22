using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : BaseTool {

	public static Fire instance = new Fire();

	// Use this for initialization
	void Start () {
		GetPrereqs ().Add (new Tuple<int, BaseResource> (0, new Flint ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, new Wood ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, new Coal ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, new IronOre ()));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, new CopperOre ()));
		SetPrereqNum (3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
