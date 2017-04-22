using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : BaseResource {

	public static Wood instance = new Wood();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add (new Tuple<int, BaseResource> (0, new Tools1()));
		SetPrereqNum (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
