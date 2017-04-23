using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour {

	public Image parentImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectResource(){
		parentImage.gameObject.SetActive (false);
		Cursor.SetCursor ((Resources.Load ("Textures/" + this.gameObject.name) as Texture2D), Vector2.zero, CursorMode.Auto);
		Camera.main.GetComponent<TerrainEditor> ().SelectBuildType (this.gameObject.name);
		// TODO:send name of button to terraineditor for enum
	}
}
