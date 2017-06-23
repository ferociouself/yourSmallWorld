using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Obfuscate : MonoBehaviour {

	public string realtext;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.GetComponent<Button> ().interactable) {
			this.GetComponentInChildren<Text> ().text = "?";
		} else {
			this.GetComponentInChildren<Text> ().text = realtext.ToUpper();
		}
	}
}
