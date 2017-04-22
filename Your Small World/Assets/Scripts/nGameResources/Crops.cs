using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : BaseResource {

	public static Crops instance = new Crops();

	// Use this for initialization
	void Start () {
		GetPrereqs().Add (new Tuple<int, BaseResource> (0, Tools2.instance));
		SetPrereqNum (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
