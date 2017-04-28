using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickedbook : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			int layerMask = 1 << 10;
			if (Physics.Raycast (ray, out hitInfo, layerMask)) {
				Debug.Log (hitInfo.collider.gameObject.name);
				if (hitInfo.collider.gameObject.name == "book") {
					ClickedBook ();
				}
			}
		}
	}

	public void ClickedBook(){
		Debug.Log ("you clicked the book!");
		this.gameObject.transform.GetComponentInParent<AudioSource> ().time = 0.0f;
		this.gameObject.transform.GetComponentInParent<AudioSource> ().Play ();
		(GameObject.FindObjectOfType (typeof(TierController)) as TierController).IncreaseTier ();
		this.gameObject.SetActive (false);
	}

	public void ShowBook(){
		this.gameObject.SetActive (true);
	}


}
