using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	bool dragging = false;

	float zoomAmt = 15.0f;

	Vector3 lastMousePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") != 0) {
			//Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 
			//		Mathf.Max(-20.0f, Mathf.Min(-7.0f, Camera.main.transform.position.z + Input.GetAxis("Mouse ScrollWheel"))));

			zoomAmt = Mathf.Min(20.0f, Mathf.Max(7.0f, zoomAmt - Input.GetAxis("Mouse ScrollWheel")));

			Vector3 awayFromSphere = Camera.main.transform.position - gameObject.transform.position;
			Camera.main.transform.position = awayFromSphere.normalized * zoomAmt + gameObject.transform.position;
		}
	}

	IEnumerator OnMouseDown() {
		dragging = true;

		lastMousePos = Input.mousePosition;

		print("Mouse Down");

		yield return null;
	}

	IEnumerator OnMouseDrag() {

		Vector3 curMousePos = Input.mousePosition;

		Vector3 mouseDelta = curMousePos - lastMousePos;

		Transform child = transform.GetChild(0);

		child.rotation = child.rotation * Quaternion.Euler(-mouseDelta.y * (zoomAmt / 100.0f), mouseDelta.x * (zoomAmt / 100.0f), 0.0f);

		lastMousePos = curMousePos;
		yield return null;
	}

	IEnumerator OnMouseUp() {
		dragging = false;

		lastMousePos = Vector3.zero;

		print("Mouse Up");

		yield return null;
	}
}
