using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseResource : MonoBehaviour {

	List<Tuple<int,BaseResource>> prereqs;
	bool unlocked;
	int numberOfPrereqs;

	void Awake(){
		prereqs = new List<Tuple<int,BaseResource>> ();
		unlocked = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Unlock(){
		unlocked = true;
	}

	public void GetUnlocked(){
		return unlocked;
	}

	public List<Tuple<int,BaseResource>> GetPrereqs(){
		return prereqs;
	}

	public List<BaseResource> GetPrereqResources(){
		List<BaseResource> br = new List<BaseResource>();
		foreach(Tuple<int, BaseResource> t in prereqs){
			br.Add (t.item2);
		}
		return br;
	}

	public int GetPrereqNum(){
		return numberOfPrereqs;
	}
}
