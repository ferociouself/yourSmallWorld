using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TierController : MonoBehaviour {

	int curTier = 0;

	UnityEvent tierIncreaseEvent;

	public Community myCommunity;

	public GameObject book;

	/// <summary>
	/// The requisites to move on from each tier.
	/// </summary>
	List<Dictionary<string,int>> tierreqs;

	// Use this for initialization
	void Awake () {
		if (tierIncreaseEvent == null)
			tierIncreaseEvent = new UnityEvent();

		/*tierreqs = new List<Dictionary<BaseResource,int>> ();

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
		tierreqs.Add (tier5);*/

		tierreqs = new List<Dictionary<string,int>> ();

		Dictionary<string,int> tier0 = new Dictionary<string,int>();
		tier0["Tree"] = 1;
		tier0["Stone"] = 3;
		tier0["Water"] = 1;

		Dictionary<string,int> tier1 = new Dictionary<string,int>();
		tier1 ["Wheat"] = 3;
		tier1 ["Water"] = 3;
		tier1 ["Iron"] = 1;
		tier1 ["Tree"] = 3;

		Dictionary<string, int> tier2 = new Dictionary<string ,int>();
		tier2 ["Coal"] = 3;
		tier2 ["Iron"] = 6;
		tier2 ["Stone"] = 9;
		tier2 ["Tree"] = 2;

		Dictionary<string, int> tier3 = new Dictionary<string, int>();
		tier3 ["Sand"] = 11;
		tier3 ["Stone"] = 12;
		tier3 ["Iron"] = 4;
		tier3 ["Wheat"] = 8;
		tier3 ["Tree"] = 5;

		Dictionary<string,int> tier4 = new Dictionary<string ,int>();
		tier4 ["Coal"] = 21;
		tier4 ["Water"] = 14;
		tier4 ["Iron"] = 15;
		tier4 ["Copper"] = 14;
		tier4 ["Stone"] = 8;
		tier4 ["Sand"] = 8;

		Dictionary<string ,int> tier5 = new Dictionary<string,int>();
		tier5 ["Oil"] = 43;
		tier5 ["Stone"] = 9;
		tier5 ["Water"] = 26;
		tier5 ["Sand"] = 28;
		tier5 ["Copper"] = 14;
		tier5 ["Tree"] = 12;
		tier5 ["Iron"] = 6;

		Dictionary<string,int> tier6 = new Dictionary<string,int>();
		tier6 ["Deiton"] = 40;
		tier6 ["Oil"] = 40;
		tier6 ["Copper"] = 40;
		tier6 ["Water"] = 40;
		tier6 ["Wheat"] = 40;
		tier6 ["Sand"] = 40;
		tier6 ["Iron"] = 40;

		tierreqs.Add (null);
		tierreqs.Add (tier0);
		tierreqs.Add (tier1);
		tierreqs.Add (tier2);
		tierreqs.Add (tier3);
		tierreqs.Add (tier4);
		tierreqs.Add (tier5);
		tierreqs.Add (tier6);

		//tierIncreaseEvent.AddListener();
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
			g = GameObject.Find ("TierName");
			if (g != null) {
				g.GetComponent<ToggleEraText> ().ChangeEraText (curTier);
				g.GetComponent<ToggleEraText> ().firstTimeInTier = true;
			}
			ResourceController cont = GetComponent<ResourceController>();
			SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
			for (int i = 0; i < terrain.vertices.Length; i++) {
				Vertex v = terrain.vertices[i];

				if (v.getBiome() == SphereTerrain.WATER_BIOME) {
					cont.WaterMade(v);
				}
				if (v.getBiome() == SphereTerrain.STONE_BIOME) {
					cont.StoneMade(v);
				}
				if (v.getBiome() == SphereTerrain.OIL_BIOME) {
					cont.OilMade(v);
				}
				if (v.getResource() != null) {
					if (v.getResource().name.Contains("Forest")) {
						cont.TreeMade(v);
					}
					if (v.getResource().name.Contains("Sand")) {
						cont.SandMade(v);
					}
					if (v.getResource().name.Contains("Wheat")) {
						cont.WheatMade(v);
					}
					if (v.getResource().name.Contains("Iron")) {
						cont.IronMade(v);
					}
					if (v.getResource().name.Contains("Copper")) {
						cont.CopperMade(v);
					}
					if (v.getResource().name.Contains("Coal")) {
						cont.CoalMade(v);
					}
				}
			}
			//TODO: check if we want all resources present in the spheremap
			tierIncreaseEvent.Invoke();
		}
	}

	/// <summary>
	/// Checks whether or not the requirements to move to the next tier are satisfied; for example: case(2) checks if all of the resources are present to move from 2 to 3
	/// </summary>
	/// <returns><c>true</c>, if tier was checked, <c>false</c> otherwise.</returns>
	public bool CheckTier(){
		if (myCommunity != null) {
			foreach (string b in tierreqs[curTier].Keys) {
				if(!myCommunity.HasGoodsCheck(b, tierreqs[curTier][b])){
					return false;
				}
			}
			book.GetComponent<clickedbook>().ShowBook ();
			return true;
		}
		return false;
	}

	/// <summary>
	/// Checks if the community wants a resource.
	/// </summary>
	/// <returns><c>true</c>, if if want was checked, <c>false</c> otherwise.</returns>
	/// <param name="b">The blue component.</param>
	public bool CheckIfWant(string b){
		if (tierreqs [curTier].ContainsKey (b)) {
			if (!myCommunity.ContainsKey (b)) {
				return true;
			}
			if (myCommunity.IsThereAFreeBoi ()) {
				return !myCommunity.HasGoodsCheck (b, tierreqs [curTier] [b]);
			}
		}
		return false;
	}

	public void FireBuilt() {
		curTier++;
		GameObject g = GameObject.Find ("TierNum");
		if (g != null) {
			Text t = g.GetComponent<Text> ();
			if (t != null) {
				t.text = curTier.ToString();
			}
		}
		g = GameObject.Find ("TierName");
		if (g != null) {
			g.GetComponent<ToggleEraText> ().ChangeEraText (curTier);
		}
		ResourceController cont = GetComponent<ResourceController>();
		SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
		for (int i = 0; i < terrain.vertices.Length; i++) {
			Vertex v = terrain.vertices[i];

			if (v.getBiome() == SphereTerrain.WATER_BIOME) {
				cont.WaterMade(v);
			}
			if (v.getBiome() == SphereTerrain.STONE_BIOME) {
				cont.StoneMade(v);
			}
			if (v.getBiome() == SphereTerrain.OIL_BIOME) {
				cont.OilMade(v);
			}
			if (v.getResource() != null) {
				if (v.getResource().name.Contains("Forest")) {
					cont.TreeMade(v);
				}
				if (v.getResource().name.Contains("Sand")) {
					cont.SandMade(v);
				}
				if (v.getResource().name.Contains("Wheat")) {
					cont.WheatMade(v);
				}
				if (v.getResource().name.Contains("Iron")) {
					cont.IronMade(v);
				}
				if (v.getResource().name.Contains("Copper")) {
					cont.CopperMade(v);
				}
				if (v.getResource().name.Contains("Coal")) {
					cont.CoalMade(v);
				}
			}
		}
	}

	/// <summary>
	/// Constucts the era at the end of each tier and clears all bois of their responsabilities.
	/// </summary>
	public void ConstructEra(){
		myCommunity.FreeBois ();
		SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
		switch (curTier) {
		case(1):
			//10 bois
			myCommunity.FreeBois();
			myCommunity.AddBois (5);
			//2 huts
			for (int i = 0; i < 3; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "wood_house");
					myCommunity.addHut (v);
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
			//unlock wheat and iron
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockWheat();
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockIron();
			break;
		case(2):
			//20 bois
			for (int i = 0; i < 2; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "wood_house");
					myCommunity.addHut (v);
				}
			}
			myCommunity.FreeBois();
			myCommunity.AddBois(10);
			//granary
			Vertex granary = myCommunity.ChooseNextBuildingLocation ();
			if (granary != null) {
				terrain.buildAtIndex (granary.getIndex (), "granary");
				myCommunity.addBuilding (granary);
			}
			//mill
			Vertex mill = myCommunity.ChooseNextBuildingLocation ();
			if (mill != null) {
				terrain.buildAtIndex (mill.getIndex (), "windmill");
				myCommunity.addBuilding (mill);
			}
			//smelter
			Vertex smelter = myCommunity.ChooseNextBuildingLocation ();
			if (smelter != null) {
				terrain.buildAtIndex (smelter.getIndex (), "smelter");
				myCommunity.addBuilding (smelter);
			}
			//unlock coal
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockCoal ();
			break;
		case(3):
			//40 bois
			//2 hut
			for (int i = 0; i < 2; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "wood_house");
					myCommunity.addHut (v);
				}
			}
			myCommunity.FreeBois();
			myCommunity.AddBois(20);
			//blast furnace
			Vertex blast = myCommunity.ChooseNextBuildingLocation ();
			if (blast != null) {
				terrain.buildAtIndex (blast.getIndex (), "blast_furnace");
				myCommunity.addBuilding (blast);
			}
			//castle
			Vertex castle = myCommunity.ChooseNextBuildingLocation ();
			if (castle != null) {
				terrain.buildAtIndex (castle.getIndex (), "tower");
				myCommunity.addBuilding (castle);
			}
			//unlock sand
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockSand ();
			break;
		case(4):
			//80 bois
			//3 huts
			for (int i = 0; i < 3; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "wood_house");
					myCommunity.addHut (v);
				}
			}
			myCommunity.FreeBois();
			myCommunity.AddBois(40);
			//observatory
			Vertex observatory = myCommunity.ChooseNextBuildingLocation ();
			if (observatory != null) {
				terrain.buildAtIndex (observatory.getIndex (), "observatory");
				myCommunity.addBuilding (observatory);
			}
			//university
			Vertex university = myCommunity.ChooseNextBuildingLocation ();
			if (university != null) {
				terrain.buildAtIndex (university.getIndex (), "temple");
				myCommunity.addBuilding (university);
			}
			//unlock copper
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockCopper ();
			break;
		case(5):
			//160 bois
			myCommunity.FreeBois();
			myCommunity.AddBois(80);
			//factory
			Vertex factory = myCommunity.ChooseNextBuildingLocation ();
			if (factory != null) {
				terrain.buildAtIndex (factory.getIndex (), "factory");
				myCommunity.addBuilding (factory);
			}
			//remove huts, replace
			for (int i = 0; i < myCommunity.getHuts ().Count; i++) {
				myCommunity.getHuts () [i].removeResource ();
				myCommunity.getHuts () [i].setIsEditable (true);
				terrain.buildAtIndex (myCommunity.getHuts () [i].getIndex (), "apartment");
			}
			//+ 4 more apartments
			for (int i = 0; i < 4; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "apartment");
					myCommunity.addBuilding (v);
				}
			}
			//unlock oil
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockOil ();
			break;
		case(6):
			//320 bois
			for (int i = 0; i < 8; i++) {
				Vertex v = myCommunity.ChooseNextBuildingLocation ();
				if (v != null) {
					terrain.buildAtIndex (v.getIndex (), "apartment");
					myCommunity.addBuilding (v);
				}
			}
			myCommunity.FreeBois();
			myCommunity.AddBois(160);
			//gas refinery
			Vertex gas = myCommunity.ChooseNextBuildingLocation ();
			if (gas != null) {
				terrain.buildAtIndex (gas.getIndex (), "refinery");
				myCommunity.addBuilding (gas);
			}
			//labs
			Vertex lab = myCommunity.ChooseNextBuildingLocation ();
			if (lab != null) {
				terrain.buildAtIndex (lab.getIndex (), "lab");
				myCommunity.addBuilding (lab);
			}
			//unlock deiton
			(GameObject.Find("HUD").GetComponent(typeof(ResourceFooBar)) as ResourceFooBar).UnlockDieton ();
			break;
		case(7):
			//same number of bois
			Vertex launchSite = myCommunity.ChooseNextBuildingLocation ();
			if (launchSite != null) {
				GameObject bigOlBoy = Resources.Load ("prefabs/bigspacefucker") as GameObject;
				GameObject realBigBoy = Instantiate (bigOlBoy, transform.TransformPoint (launchSite.getSphereVector () * 1.3f), Quaternion.identity);
				realBigBoy.GetComponent<Rigidbody> ().velocity = transform.TransformPoint(launchSite.getSphereVector ()).normalized * 0.5f;
			} else {
				GameObject bigOlBoy = Resources.Load ("prefabs/bigspacefucker") as GameObject;
				GameObject realBigBoy = Instantiate (bigOlBoy, transform.TransformPoint (myCommunity.getCampfireVertex ().getSphereVector () * 1.3f), Quaternion.identity);
				realBigBoy.GetComponent<Rigidbody> ().velocity = transform.TransformPoint(myCommunity.getCampfireVertex ().getSphereVector ()).normalized * 0.5f;
			}
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
