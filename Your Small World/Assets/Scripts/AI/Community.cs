using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Community : MonoBehaviour {

	private Dictionary<BaseResource,int> goods;
	int bois;
	int busybois;

	// Use this for initialization
	void Start () {
		goods = new Dictionary<BaseResource, int> ();
		bois = 5;
		busybois = 0;
		//TODO: make the bois
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddBois(int toAdd){
		toAdd = Mathf.Abs (toAdd);
		bois += toAdd;
		//TODO: make more bois
	}

	public int GetBois(){
		return bois;
	}

	public bool MakeBusyBoi(){
		if (busybois < bois) {
			busybois++;
			return true;
		}
		return false;
	}

	public int GetBusyBois(){
		return busybois;
	}

	public bool IsThereAFreeBoi(){
		return busybois < bois;
	}

	public void FreeBois(){
		busybois = 0;
	}

	/// <summary>
	/// Adds the goods.
	/// </summary>
	/// <returns><c>true</c>, if goods was added, <c>false</c> otherwise.</returns>
	/// <param name="good">Good.</param>
	/// <param name="amountToAdd">Amount to add.</param>
	public bool AddGoods(BaseResource good, int amountToAdd){
		if (goods.ContainsKey (good)) {
			goods.Add (good, goods [good] + amountToAdd);
			return true;
		}
		goods.Add (good, amountToAdd);
		return false;
	}

	/// <summary>
	/// Removes the goods if they can be removes; otherwise returns false.
	/// </summary>
	/// <returns><c>true</c>, if goods was removed, <c>false</c> otherwise.</returns>
	/// <param name="good">Good.</param>
	/// <param name="amountToRemove">Amount to remove.</param>
	public bool RemoveGoods(BaseResource good, int amountToRemove){
		if (HasGoodsCheck(good, amountToRemove)) {
			goods.Add (good, goods[good] - amountToRemove);
			return true;
		}
		return false;
	}

	/// <summary>
	/// Determines whether this instance has goods of at least the specified good amount.
	/// </summary>
	/// <returns><c>true</c> if this instance has goods check the specified good amount; otherwise, <c>false</c>.</returns>
	/// <param name="good">Good to check</param>
	/// <param name="amount">Amount.</param>
	public bool HasGoodsCheck(BaseResource good, int amount){
		if (goods.ContainsKey (good) && goods [good] - amount >= 0)
			return true;
		return false;
	}

	public bool ContainsKey(BaseResource good){
		return goods.ContainsKey (good);
	}

	public Vertex ChooseNextBuildingLocation() {
		SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
		Vertex[] buildingIndices = terrain.currentBuildings ();
		int randomBuildingIndex = Random.Range (0, buildingIndices.Length);
		int numBuildingsChecked = 0;
		while (numBuildingsChecked < buildingIndices.Length) {
			Vertex randomBuilding = buildingIndices [randomBuildingIndex];
			Vertex[] neighbors = randomBuilding.getNeighbors ();
			int randomNeighborIndex = Random.Range (0, neighbors.Length);
			int numNeighborsChecked = 0;
			while (numNeighborsChecked < neighbors.Length) {
				if (neighbors [randomNeighborIndex].getHeight() == 0 && neighbors[randomNeighborIndex].getResource() == null) {
					return neighbors [randomNeighborIndex];
				}
				randomNeighborIndex = (randomNeighborIndex + 1) % neighbors.Length;
				numNeighborsChecked++;
			}
			randomBuildingIndex = (randomBuildingIndex + 1) % buildingIndices.Length;
			numBuildingsChecked++;
		}
		return null;
	}
}
