using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainEditor : MonoBehaviour {

	public SphereTerrain st;

	public Text dirIndicator;

	float buffer = 0.0f;
	public float maxBuffer = 1.0f;

	int incrDir = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1) && buffer > maxBuffer) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			int layerMask = 1 << 8;
			if (Physics.Raycast(ray, out hitInfo, layerMask)) {
				st.incHeightAtIndex(st.findIndexOfNearest(hitInfo.point), incrDir * 0.1f);
			}
			buffer = 0.0f;
		}
		if (Input.GetButtonDown("Activate")) {
			incrDir *= -1;
			if (incrDir > 0) {
				dirIndicator.text = "Direction: Up";
			} else {
				dirIndicator.text = "Direction: Down";
			}
		}
		buffer += Time.deltaTime;
	}
}
