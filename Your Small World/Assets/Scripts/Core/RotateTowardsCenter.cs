using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCenter : MonoBehaviour {

	public Transform center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 towardsCenter = center.position - transform.position;

		transform.rotation = Quaternion.LookRotation(towardsCenter) * Quaternion.Euler(-90.0f, 0.0f, 0.0f);
	}
}
