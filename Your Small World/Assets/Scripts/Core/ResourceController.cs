﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResourceMade(BaseResource b) {
		if (GetComponent<TierController>().CheckIfWant(b)) {

		}
	}
}