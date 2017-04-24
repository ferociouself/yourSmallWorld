using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TierController : MonoBehaviour {

	int curTier = 0;

	UnityEvent tierIncreaseEvent;

	public Community myCommunity;

	/// <summary>
	/// The requisites to move on from each tier.
	/// </summary>
	List<Dictionary<BaseResource,int>> tierreqs;

	// Use this for initialization
	void Start () {
		if (tierIncreaseEvent == null)
			tierIncreaseEvent = new UnityEvent();

		tierreqs = new List<Dictionary<BaseResource,int>> ();

		Dictionary<BaseResource,int> tier0 = new Dictionary<BaseResource,int>();
		tier0[Tree.instance] = 1;
		tier0[Stone.instance] = 3;
		tier0[Water.instance] = 1;

		Dictionary<BaseResource,int> tier1 = new Dictionary<BaseResource,int>();
		tier1 [Wheat.instance] = 3;
		tier1 [Water.instance] = 3;
		tier1 [Iron.instance] = 1;
		tier1 [Tree.instance] = 3;

		Dictionary<BaseResource, int> tier2 = new Dictionary<BaseResource ,int>();
		tier2 [Coal.instance] = 3;
		tier2 [Iron.instance] = 6;
		tier2 [Stone.instance] = 9;
		tier2 [Tree.instance] = 2;

		Dictionary<BaseResource,int> tier3 = new Dictionary<BaseResource ,int>();
		tier3 [Coal.instance] = 21;
		tier3 [Water.instance] = 14;
		tier3 [Iron.instance] = 15;
		tier3 [Copper.instance] = 14;
		tier3 [Stone.instance] = 8;
		tier3 [Sand.instance] = 8;

		Dictionary<BaseResource ,int> tier4 = new Dictionary<BaseResource,int>();
		tier4 [Oil.instance] = 43;
		tier4 [Stone.instance] = 9;
		tier4 [Water.instance] = 26;
		tier4 [Sand.instance] = 28;
		tier4 [Copper.instance] = 14;
		tier4 [Tree.instance] = 12;
		tier4 [Iron.instance] = 6;

		Dictionary<BaseResource,int> tier5 = new Dictionary<BaseResource,int>();
		tier5 [Deiton.instance] = 40;
		tier5 [Oil.instance] = 40;
		tier5 [Copper.instance] = 40;
		tier5 [Water.instance] = 40;
		tier5 [Wheat.instance] = 40;
		tier5 [Sand.instance] = 40;
		tier5 [Iron.instance] = 40;

		tierreqs.Add (tier0);
		tierreqs.Add (tier1);
		tierreqs.Add (tier2);
		tierreqs.Add (tier3);
		tierreqs.Add (tier4);
		tierreqs.Add (tier5);

		//tierIncreaseEvent.AddListener();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			ConstructEra ();
		}
	}

	public void IncreaseTier() {
		if (CheckTier()) {
			ConstructEra ();
			curTier++;
			GameObject g = GameObject.Find ("TierNum");
			if (g != null) {
				Text t = g.GetComponent<Text> ();
				if (t != null) {
					t.text = curTier.ToString();
				}
			}
			//TODO: change the TierText as well!!!
			//TODO: check if we want all resources present in the spheremap
			tierIncreaseEvent.Invoke();
		}
	}

	/// <summary>
	/// Checks whether or not the requirements to move to the next tier are satisfied; for example: case(2) checks if all of the resources are present to move from 2 to 3
	/// </summary>
	/// <returns><c>true</c>, if tier was checked, <c>false</c> otherwise.</returns>
	private bool CheckTier(){
		if (myCommunity != null) {
			foreach (BaseResource b in tierreqs[curTier].Keys) {
				if(!myCommunity.HasGoodsCheck(b, tierreqs[curTier][b])){
					return false;
				}
			}
			return true;
		}
		return false;
	}

	/// <summary>
	/// Checks if the community wants a resource.
	/// </summary>
	/// <returns><c>true</c>, if if want was checked, <c>false</c> otherwise.</returns>
	/// <param name="b">The blue component.</param>
	public bool CheckIfWant(BaseResource b){
		if (tierreqs [curTier].ContainsKey (b)) {
			if (!myCommunity.ContainsKey (b)) {
				return true;
			}
			if (myCommunity.IsThereAFreeBoi ()) {
				return !myCommunity.HasGoodsCheck (b, tierreqs [curTier] [b]);
				//TODO: if true go get resource and mark a boi as busy
			}
		}
		return false;
	}

	/// <summary>
	/// Constucts the era at the end of each tier and clears all bois of their responsabilities.
	/// </summary>
	public void ConstructEra(){
		myCommunity.FreeBois ();
		SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
		switch (curTier) {
		case(0):
			//10 bois
			//2 huts
			for (int i = 0; i < 2; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "wood_house");
					myCommunity.addBuilding (v);
				}
			}
			//lithic workshop
			Vertex litho = myCommunity.ChooseNextBuildingLocation ();
			if (litho != null) {
				terrain.buildAtIndex (litho.getIndex (), "stonecraft_house");
				myCommunity.addBuilding (litho);
			}
			//well
			Vertex well = myCommunity.ChooseNextBuildingLocation ();
			if (well != null) {
				terrain.buildAtIndex (well.getIndex (), "well");
				myCommunity.addBuilding (well);
			}
			break;
		case(1):
			//20 bois
			//granary
			//mill
			//smelter
			break;
		case(2):
			//40 bois
			//blast furnace
			//castle
			break;
		case(3):
			//80 bois
			//observatory
			//university
			break;
		case(4):
			//160 bois
			//factory
			//gettos (the projects)
			break;
		case(5):
			//320 bois
			//gas refinery
			//labs
			break;
		case(6):
			//same number of bois
			//space station
			break;
		default:
			//5 bois
			//fire
			break;
		}
	}

	/// <summary>
	/// Sets the community.
	/// </summary>
	/// <param name="commune">Commune.</param>
	public void SetCommunity(Community commune){
		this.myCommunity = commune;
	}

	/// <summary>
	/// Gets the community.
	/// </summary>
	/// <returns>The community.</returns>
	public Community GetCommunity(){
		return myCommunity;
	}
}
