using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

		/*Dictionary<BaseResource,int> tier0 = new Dictionary<BaseResource,int>();
		tier0.Add (Tree.instance, 1);
		tier0.Add (Stone.instance, 3);
		tier0.Add (Water.instance, 1);

		Dictionary<BaseResource,int> tier1 = new Dictionary<BaseResource,int>();
		tier1.Add (Wheat.instance, 3);
		tier1.Add (Water.instance, 3);
		tier1.Add (Iron.instance, 1);
		tier1.Add (Tree.instance, 3);

		Dictionary<BaseResource,int> tier2 = new Dictionary<BaseResource,int>();
		tier2.Add (Coal.instance, 3);
		tier2.Add (Iron.instance, 6);
		tier2.Add (Stone.instance, 9);
		tier2.Add (Tree.instance, 2);

		Dictionary<BaseResource,int> tier3 = new Dictionary<BaseResource,int>();
		tier3.Add (Coal.instance, 21);
		tier3.Add (Water.instance, 14);
		tier3.Add (Iron.instance, 15);
		tier3.Add (Copper.instance, 14);
		tier3.Add (Stone.instance, 8);
		tier3.Add (Sand.instance, 8);

		Dictionary<BaseResource,int> tier4 = new Dictionary<BaseResource,int>();
		tier4.Add (Oil.instance, 43);
		tier4.Add (Stone.instance, 9);
		tier4.Add (Water.instance, 26);
		tier4.Add (Sand.instance, 28);
		tier4.Add (Copper.instance, 14);
		tier4.Add (Tree.instance, 12);
		tier4.Add (Iron.instance, 6);

		Dictionary<BaseResource,int> tier5 = new Dictionary<BaseResource,int>();
		tier5.Add (Deiton.instance, 40);
		tier5.Add (Oil.instance, 40);
		tier5.Add (Copper.instance, 40);
		tier5.Add (Water.instance, 40);
		tier5.Add (Wheat.instance, 40);
		tier5.Add (Sand.instance, 40);
		tier5.Add (Iron.instance, 40);

		tierreqs.Add (tier0);
		tierreqs.Add (tier1);
		tierreqs.Add (tier2);
		tierreqs.Add (tier3);
		tierreqs.Add (tier4);
		tierreqs.Add (tier5);

		//tierIncreaseEvent.AddListener();*/
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
	private bool CheckIfWant(BaseResource b){
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
