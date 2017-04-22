using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools2 : BaseTool {

	public static Tools2 instance = new Tools2();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new Tools1()));
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new Iron()));
		SetPrereqNum (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
