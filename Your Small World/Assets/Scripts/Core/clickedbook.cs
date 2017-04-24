using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickedbook : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ClickedBook(){
		Debug.Log ("you clicked the book!");
		this.gameObject.GetComponent<AudioSource> ().Play ();
	}
}
