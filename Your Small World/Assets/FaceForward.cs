using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<TextMesh> ().text = "qo?";
		//transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation * Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		//this.gameObject.transform.LookAt (Camera.main.transform);
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
		//this.gameObject.transform.localScale = new Vector3 (-1 * this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
	}
}
