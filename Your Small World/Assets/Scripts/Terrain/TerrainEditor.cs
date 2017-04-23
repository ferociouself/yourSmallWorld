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
		Terrain,
		Water,
		Stone,
		Sand,
		Tree,
		Wheat,
		Oil,
		Iron,
		Copper,
		Coal,
		Deiton
	}

	BuildType curType;

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
		curType = BuildType.Terrain;
		incrDir = Mathf.Abs (incrDir);
	}

	public void GoingDown(){
		curType = BuildType.Terrain;
		incrDir =-1 * Mathf.Abs (incrDir);
	}

	public void SelectBuildType(string type) {
		switch(type) {
		case "Water":
			curType = BuildType.Water;
			break;
		case "Stone":
			curType = BuildType.Stone;
			break;
		case "Sand":
			curType = BuildType.Sand;
			break;
		case "Tree":
			curType = BuildType.Tree;
			break;
		case "Wheat":
			curType = BuildType.Wheat;
			break;
		case "Oil":
			curType = BuildType.Oil;
			break;
		case "Iron":
			curType = BuildType.Iron;
			break;
		case "Copper":
			curType = BuildType.Copper;
			break;
		case "Coal":
			curType = BuildType.Coal;
			break;
		case "Deiton":
			curType = BuildType.Deiton;
			break;
		default:
			Debug.LogError("Build Type of " + type + " not recognized!");
		}
	}

}
