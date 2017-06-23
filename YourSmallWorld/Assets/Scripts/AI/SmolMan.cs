using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowPath))]
public class SmolMan : MonoBehaviour {

	public bool resourceActive = false;
	Vertex targetResource;

	Community comm;

	// Use this for initialization
	void Awake () {
		comm = GameObject.FindObjectOfType(typeof(Community)) as Community;
		if (comm != null) {
			GetComponent<FollowPath> ().start = comm.getCampfireVertex();
			GetComponent<FollowPath> ().targetGoal = comm.getCampfireVertex();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setResource(Vertex res) {
		targetResource = res;
		resourceActive = true;
	}

	public void setTarget(Vertex res) {
		GetComponent<FollowPath>().backAndForth = true;
		GetComponent<FollowPath>().targetGoal = res;
	}

	public Community getCommunity() {
		return comm;
	}

	public void findNewBuilding() {
		if (comm == null) {
			Debug.LogError("Comm is null");
			comm = GameObject.FindObjectOfType(typeof(Community)) as Community;
		}
		List<Vertex> buildings = new List<Vertex>(comm.getBuildingLocations());
		buildings.Remove(comm.getCampfireVertex());
		if (buildings.Count == 0) {
			GetComponent<FollowPath>().backAndForth = false;
			GetComponent<FollowPath>().targetGoal = comm.getCampfireVertex();
		} else {
			int randIndex = Random.Range(0, buildings.Count);
			GetComponent<FollowPath>().backAndForth = false;
			GetComponent<FollowPath>().targetGoal = buildings[randIndex];
		}
		resourceActive = false;
		targetResource = null;
	}

	public Vertex getResource() {
		if (this.resourceActive) {
			return targetResource;
		} else {
			return null;
		}
	}



}
