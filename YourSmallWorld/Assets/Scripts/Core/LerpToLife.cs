using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToLife : MonoBehaviour {

	float curTimer = 0.0f;
	public float maxTimer = 0.5f;

	public Vector3 finalScale;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		curTimer += Time.deltaTime;

		transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, curTimer/maxTimer);

		if (curTimer > maxTimer) {
			DestroyImmediate(this);
		}
	}
}
