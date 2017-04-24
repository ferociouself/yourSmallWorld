using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Community : MonoBehaviour {

	private Dictionary<BaseResource,int> goods;
	int bois;
	int busybois;

	private List<Vertex> buildingLocations;
	private Vertex campfireVertex;

	// Use this for initialization
	void Start () {
		if (buildingLocations == null) {
			buildingLocations = new List<Vertex> ();
		}
		goods = new Dictionary<BaseResource, int> ();
		bois = 5;
		busybois = 0;
		SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
		setCampfireVertex (terrain.getVertex (terrain.findIndexOfNearest (gameObject.transform.position)));
		//TODO: make the bois
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setCampfireVertex(Vertex v) {
		campfireVertex = v;
		addBuilding (v);
	}

	public Vertex getCampfireVertex() {
		return campfireVertex;
	}

	public List<Vertex> getBuildingLocations() {
		return buildingLocations;
	}

	public void addBuilding(Vertex v) {
		buildingLocations.Add (v);
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
		int randomBuildingIndex = Random.Range (0, buildingLocations.Count);
		for (int i = 0; i < buildingLocations.Count; i++) {
			int numNeighbors = buildingLocations [(i + randomBuildingIndex) % buildingLocations.Count].getNeighbors ().Length;
			int randomNeighborIndex = Random.Range (0, numNeighbors);
			for (int j = 0; j < numNeighbors; j++) {
				Vertex potential = buildingLocations [(i + randomBuildingIndex) % buildingLocations.Count].getNeighbors () [(j + randomNeighborIndex) % numNeighbors];
				if (potential.getHeight () == 0 && potential.getIsEditable()) {
					return potential;
				}
			}
		}
		return null;
	}

	void OnDrawGizmos() {
		if (campfireVertex != null) {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere (campfireVertex.getSphereVector(), 0.2f);
		}
	}
}
