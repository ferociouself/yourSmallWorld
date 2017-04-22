using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	public float Rate;
	public GameObject planet;
	float delay;

	// Use this for initialization
	void Start () {
		delay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (delay >= 1 / Rate) {
			makeWater ();
			delay = 0;
		}
		delay += Time.deltaTime;
	}

	void makeWater() {
		GameObject waterball = Resources.Load ("prefabs/water") as GameObject;
		GameObject realBall = GameObject.Instantiate (waterball);
		realBall.transform.position = this.transform.position;
		realBall.GetComponent<GravityAffected> ().centerOfGravity = planet;
	}
}
