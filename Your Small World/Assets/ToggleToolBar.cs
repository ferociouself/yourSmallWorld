using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleToolBar : MonoBehaviour {

	public Image tools;
	public Image toolDisable;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActivateTools(){
		tools.gameObject.SetActive(true);
		toolDisable.gameObject.SetActive (false);
	}

	public void DisableTools(){
		tools.gameObject.SetActive (false);
	}
}
