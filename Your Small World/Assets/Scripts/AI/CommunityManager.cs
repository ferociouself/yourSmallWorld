using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityManager : MonoBehaviour {

	private static CommunityManager s_instance;
	private List<Community> communities;

	public static CommunityManager GetInstance(){
		if (s_instance == null) {
			s_instance = new CommunityManager ();
		}
		return s_instance;
	}

	// Use this for initialization
	void Start () {
		communities = new List<Community> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RegisterCommunity(Community commune){
		communities.Add (commune);
	}

	public List<Community> GetCommunities(){
		return this.communities;
	}

	public int GetNumberOfCommunities(){
		return this.communities.Count ;
	}
}
