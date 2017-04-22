using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools3 : BaseTool {

	public static Tools3 instance = new Tools3();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add (new Tuple<int, BaseResource> (0, Tools2.instance));
		GetPrereqs ().Add (new Tuple<int, BaseResource> (1, Steel.instance));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
