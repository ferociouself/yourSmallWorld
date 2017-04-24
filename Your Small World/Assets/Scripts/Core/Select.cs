using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour {

	public Image parentImage;

	Sprite img;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectResource(){
		img = Resources.Load<Sprite>("Cursors/" + this.gameObject.name);
		if (img != null) {
			Camera.main.GetComponent<TerrainEditor> ().SelectBuildType (this.gameObject.name);
			GameObject.Find ("ResourceImg").GetComponent<Image> ().sprite = img;
		}
		parentImage.gameObject.SetActive (false);
	}

	public void SelectUpOrDown(){
		img = Resources.Load<Sprite>("Cursors/" + this.gameObject.name);
		if (img != null) {
			GameObject.Find ("ResourceImg").GetComponent<Image> ().sprite = img;
		}
		parentImage.gameObject.SetActive (false);
	}
}
