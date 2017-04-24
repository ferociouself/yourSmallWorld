using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSender : MonoBehaviour {

	public string s;

	// Use this for initialization
	void Start () {
		if (s != "") {
			QuestTexter.GetInstance().UpdateText (s);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
