using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainEditor : MonoBehaviour {

	public SphereTerrain st;

	public Text dirIndicator;
	public Text buildIndicator;

	bool downInPreviousFrame = false;
	bool isDragActive = false;

	float buffer = 0.0f;
	public float maxBuffer = 1.0f;

	int incrDir = 1;

	enum BuildType {
		Terrain,
		Smooth,
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

	BuildType curType = BuildType.Smooth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			int layerMask = 1 << 8;
			if (Physics.Raycast(ray, out hitInfo, layerMask)) {
				if (downInPreviousFrame)
				{
					if (isDragActive)
					{
						//(GameObject.FindObjectOfType(typeof(MusicController)) as MusicController).PlaceSingle();
					}
					else
					{
						isDragActive = true;
						//(GameObject.FindObjectOfType(typeof(MusicController)) as MusicController).StartPlacing();
					}
				}
				downInPreviousFrame = true;
			}
		}
		else
		{
			if (isDragActive)
			{
				isDragActive = false;
				(GameObject.FindObjectOfType(typeof(MusicController)) as MusicController).SetFading();
			}
			downInPreviousFrame = false;
		}

		if (Input.GetMouseButton(0) && buffer > maxBuffer) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			int layerMask = 1 << 8;
			if (Physics.Raycast(ray, out hitInfo, layerMask)) {
				switch (curType) {
				case BuildType.Terrain:
					st.incHeightAtIndex(st.findIndexOfNearest(hitInfo.point), incrDir * 0.1f);
					break;
				case BuildType.Smooth:
					st.setHeightAtIndex(st.findIndexOfNearest(hitInfo.point), 0.0f);
					break;
				case BuildType.Water:
					st.waterAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Stone:
					st.StoneAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Sand:
					st.SandAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Tree:
					st.TreeAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Wheat:
					st.WheatAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Oil:
					st.OilAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Iron:
					st.IronAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Copper:
					st.CopperAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Coal:
					st.CoalAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				case BuildType.Deiton:
					st.DeitonAtIndex(st.findIndexOfNearest(hitInfo.point));
					break;
				default:
					break;
				}

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

	public void NotGoingAnywhere(){
		curType = BuildType.Smooth;
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
			Debug.LogError ("Build Type of " + type + " not recognized!");
			break;
		}
	}

}
