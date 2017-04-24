using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class QuestTexter : MonoBehaviour {

	private static QuestTexter instance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static QuestTexter GetInstance(){
		if (instance == null) {
			instance = GameObject.Find("TierName").GetComponent<QuestTexter>();
		}
		return instance;
	}

	public void UpdateText(string t){
		Text tex = GetInstance().gameObject.GetComponent<Text> ();
		if (tex == null) {
			this.gameObject.AddComponent (typeof(Text));
			tex = this.gameObject.GetComponent<Text> ();
		} 
		tex.text = t;
	}
}
