using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GravityAffected : MonoBehaviour {

	public GameObject centerOfGravity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody> ().AddExplosionForce (-1, centerOfGravity.transform.position, 100);
		if ((this.gameObject.transform.position - centerOfGravity.transform.position).magnitude < 0.5f) {
			Destroy (gameObject);
		}
	}
}
