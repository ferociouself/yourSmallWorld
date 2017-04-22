using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour {

	public float rotationInSeconds = 300.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(((360/rotationInSeconds) * Time.deltaTime), 0.0f, 0.0f);
	}
}
