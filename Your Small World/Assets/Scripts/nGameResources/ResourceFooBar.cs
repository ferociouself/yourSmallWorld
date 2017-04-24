using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFooBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UnlockWater(){
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
	}

	public void UnlockStone(){
		gameObject.transform.GetChild (1).gameObject.SetActive (true);
	}

	public void UnlockSand(){
		gameObject.transform.GetChild (2).gameObject.SetActive (true);
	}

	public void UnlockTree(){
		gameObject.transform.GetChild (3).gameObject.SetActive (true);
	}

	public void UnlockWheat(){
		gameObject.transform.GetChild (4).gameObject.SetActive (true);
	}

	public void UnlockOil(){
		gameObject.transform.GetChild (5).gameObject.SetActive (true);
	}

	public void UnlockIron(){
		gameObject.transform.GetChild (6).gameObject.SetActive (true);
	}

	public void UnlockCopper(){
		gameObject.transform.GetChild (7).gameObject.SetActive (true);
	}

	public void UnlockCoal(){
		gameObject.transform.GetChild (8).gameObject.SetActive (true);
	}

	public void UnlockDieton(){
		gameObject.transform.GetChild (9).gameObject.SetActive (true);
	}
}
