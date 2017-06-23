using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceFooBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UnlockWater(){
		gameObject.transform.GetChild (7).GetChild (0).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockStone(){
		gameObject.transform.GetChild (7).GetChild (1).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockSand(){
		gameObject.transform.GetChild (7).GetChild (2).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockTree(){
		gameObject.transform.GetChild (7).GetChild (3).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockWheat(){
		gameObject.transform.GetChild (7).GetChild (4).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockOil(){
		gameObject.transform.GetChild (7).GetChild (5).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockIron(){
		gameObject.transform.GetChild (7).GetChild (6).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockCopper(){
		gameObject.transform.GetChild (7).GetChild (7).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockCoal(){
		gameObject.transform.GetChild (7).GetChild (8).gameObject.GetComponent<Button> ().interactable = true;
	}

	public void UnlockDieton(){
		gameObject.transform.GetChild (7).GetChild (9).gameObject.GetComponent<Button> ().interactable = true;
	}
}
