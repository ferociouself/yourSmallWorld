using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seeya : MonoBehaviour {

	public Image toolbar;
	public Image toolbar2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(toolbar != null && toolbar2 != null){
			if (Input.mousePosition.x < toolbar.transform.position.x - 100) {
				toolbar.gameObject.SetActive (false);
				toolbar2.gameObject.SetActive (false);
			}
		}
	}
}
