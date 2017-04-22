using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : BaseTool {

	public static Fire instance = new Fire();

	// Use this for initialization
	void Start () {
		GetPrereqs ().Add (new Tuple<int, BaseResource> (0, Flint.instance));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, Wood.instance));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, Coal.instance));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, IronOre.instance));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (2, CopperOre.instance));
		SetPrereqNum (3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
