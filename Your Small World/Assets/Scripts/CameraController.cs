using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	bool dragging = false;

	Vector3 lastMousePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

		Vector3 rotDelta = new Vector3(-mouseDelta.y, mouseDelta.x, 0.0f);

		print("Cur Rotation: " + child.rotation);
		print("Rot Delta: " + rotDelta);
		print("Rot Delta Rotation: " + Quaternion.Euler(-mouseDelta.y , mouseDelta.x, 0.0f));

		child.rotation = child.rotation * Quaternion.Euler(-mouseDelta.y , mouseDelta.x, 0.0f);
		//child.rotation = Quaternion.Euler(child.rotation.eulerAngles.x, child.rotation.eulerAngles.y, 0.0f);

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
