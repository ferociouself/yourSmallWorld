using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools1 : BaseTool {

	public static Tools1 instance = new Tools1();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, Flint.instance));
		SetPrereqNum (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
