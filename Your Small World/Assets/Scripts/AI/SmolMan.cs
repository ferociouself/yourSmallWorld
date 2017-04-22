using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolMan : MonoBehaviour {

	public float speed = 1.0f;
	public float rotateSpeed = 1.0f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			rb.AddRelativeForce(Vector3.forward * speed);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(0.0f, rotateSpeed, 0.0f);
			//rb.AddRelativeForce(Vector3.right * speed);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(0.0f, -rotateSpeed, 0.0f);
			//rb.AddRelativeForce(Vector3.left * speed);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			rb.AddRelativeForce(Vector3.back * speed);
		}
	}
}
