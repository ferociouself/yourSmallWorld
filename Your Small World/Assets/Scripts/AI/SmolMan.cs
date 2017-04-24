﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolMan : MonoBehaviour {

	bool resourceActive = false;

	Community comm;

	SphereTerrain st;

	// Use this for initialization
	void Start () {
		st = GameObject.FindObjectOfType(typeof(SphereTerrain)) as SphereTerrain;
		comm = GameObject.FindObjectOfType(typeof(Community)) as Community;
		GetComponent<FollowPath>().start = st.getVertex(st.findIndexOfNearest(GameObject.Find("Campfire").transform.position));
		GetComponent<FollowPath>().targetGoal = st.getVertex(st.findIndexOfNearest(GameObject.Find("Building").transform.position));
		if (comm == null) {
			Debug.LogError("Community does not exist!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setResource(Vertex res) {
		GetComponent<FollowPath>().backAndForth = true;
		GetComponent<FollowPath>().targetGoal = res;
		resourceActive = true;
	}

	public void findNewBuilding() {
		Debug.Log("Finding new Building!");

		List<Vertex> buildings = comm.getBuildingLocations();
		int randIndex = Random.Range(0, buildings.Count);

		GetComponent<FollowPath>().targetGoal = buildings[randIndex];

		//GetComponent<FollowPath>().targetGoal = new building
	}


}
