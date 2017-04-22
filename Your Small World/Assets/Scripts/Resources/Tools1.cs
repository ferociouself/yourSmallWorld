using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools1 : BaseTool {
	
	// Use this for initialization
	void Start () {
		GetPrereqs().Add(new Tuple<int, BaseResource>(1, new Flint()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
