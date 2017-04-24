using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<TextMesh> ().text = "qo?";
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}
}
