using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResource : MonoBehaviour {

	List<BaseResource> prereqs;
	bool unlocked;

	void Awake(){
		prereqs = new List<BaseResource> ();
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

	public List<BaseResource> GetPrereqs(){
		return prereqs;
	}
}
