using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TierController : MonoBehaviour {

	int curTier = 0;

	UnityEvent tierIncreaseEvent;

	// Use this for initialization
	void Start () {
		if (tierIncreaseEvent == null)
			tierIncreaseEvent = new UnityEvent();

		//tierIncreaseEvent.AddListener();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void increaseTier() {
		curTier++;

		tierIncreaseEvent.Invoke();
	}
}
