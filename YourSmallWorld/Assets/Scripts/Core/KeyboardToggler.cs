using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardToggler : MonoBehaviour {

	Sprite img;

	// Use this for initialization
	void Start () {
		if (!MusicController.introduction) {
			if (Input.GetButtonDown ("1")) {
				Debug.Log ("pressed 1");
				SelectResource ("Water");
			}
			if (Input.GetButtonDown ("2")) {
				SelectResource ("Stone");
			}
			if (Input.GetButtonDown ("3")) {
				SelectResource ("Sand");
			}
			if (Input.GetButtonDown ("4")) {
				SelectResource ("Tree");
			}
			if (Input.GetButtonDown ("5")) {
				SelectResource ("Wheat");
			}
			if (Input.GetButtonDown ("6")) {
				SelectResource ("Oil");
			}
			if (Input.GetButtonDown ("7")) {
				SelectResource ("Iron");
			}
			if (Input.GetButtonDown ("8")) {
				SelectResource ("Copper");
			}
			if (Input.GetButtonDown ("9")) {
				SelectResource ("Coal");
			}
			if (Input.GetButtonDown ("0")) {
				SelectResource ("Deiton");
			}
			if (Input.GetButtonDown ("-")) {
				SelectUpOrDown ("Lower");
			}
			if (Input.GetButtonDown ("=")) {
				SelectUpOrDown ("Raise");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectResource(string name){
		img = Resources.Load<Sprite>("Cursors/" + name);
		if (img != null) {
			Camera.main.GetComponent<TerrainEditor> ().SelectBuildType (name);
			GameObject.Find ("ResourceImg").GetComponent<Image> ().sprite = img;
		}
	}

	public void SelectUpOrDown(string name){
		img = Resources.Load<Sprite>("Cursors/" + name);
		if (img != null) {
			GameObject.Find ("ResourceImg").GetComponent<Image> ().sprite = img;
			if (name == "Lower") {
				(GameObject.FindObjectOfType<TerrainEditor> () as TerrainEditor).GoingDown ();
			} else if (name == "Raise") {
				(GameObject.FindObjectOfType<TerrainEditor> () as TerrainEditor).GoingUp ();
			}
		}
	}
}
