using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainEditor : MonoBehaviour {

	public SphereTerrain st;

	public Text dirIndicator;
	public Text buildIndicator;

	float buffer = 0.0f;
	public float maxBuffer = 1.0f;

	int incrDir = 1;

	enum BuildType {

	}

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

		buffer += Time.deltaTime;
	}

	public void GoingUp(){
		incrDir = Mathf.Abs (incrDir);
	}

	public void GoingDown(){
		incrDir =-1 * Mathf.Abs (incrDir);
	}


}
