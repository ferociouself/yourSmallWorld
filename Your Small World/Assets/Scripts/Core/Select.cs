using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour {

	public Image parentImage;

	Vector2 cursorHotspot;
	Texture2D t2d;
	bool done;
	bool once;

	// Use this for initialization
	void Start () {
		done = false;
		once = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (done) {
			StopCoroutine (SetInitialCursor ());
			if (once) {
				parentImage.gameObject.SetActive (false);
				once = false;
			}
		}
	}

	public void SelectResource(){
		t2d = Resources.Load ("Cursors/" + this.gameObject.name) as Texture2D;
		if (t2d != null) {
			cursorHotspot = new Vector2 (t2d.width / 2, t2d.height / 2);
			//Cursor.SetCursor (t2d, cursorHotspot, CursorMode.Auto);
			Camera.main.GetComponent<TerrainEditor> ().SelectBuildType (this.gameObject.name);
			done = false;
			once = true;
			StartCoroutine (SetInitialCursor ());
		}
	}

	public void SelectUpOrDown(){
		t2d = Resources.Load ("Cursors/" + this.gameObject.name) as Texture2D;
		if (t2d != null) {
			cursorHotspot = new Vector2 (t2d.width / 2, t2d.height / 2);
			//Cursor.SetCursor (t2d, cursorHotspot, CursorMode.Auto);
			done = false;
			once = true;
			StartCoroutine (SetInitialCursor ());
		}
	}
		
	private IEnumerator SetInitialCursor() {
		yield return null;
		Cursor.SetCursor(t2d, cursorHotspot, CursorMode.Auto);
		done = true;
	}
}
