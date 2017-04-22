using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : BaseResource {

	public static Meat instance = new Meat();

	// Use this for initialization
	void Start () {
		SetPrereqNum (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
