using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools2 : BaseTool {

	public static Tools2 instance = new Tools2();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, Tools1.instance));
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, Iron.instance));
		SetPrereqNum (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
