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
		Texture2D t2d = Resources.Load ("Textures/" + this.gameObject.name) as Texture2D;
		Vector2 cursorHotspot = new Vector2 (t2d.width / 2, t2d.height / 2);
		Cursor.SetCursor (t2d, cursorHotspot, CursorMode.Auto);
		Camera.main.GetComponent<TerrainEditor> ().SelectBuildType (this.gameObject.name);
	}
}
