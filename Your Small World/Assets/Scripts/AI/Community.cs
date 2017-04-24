using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Community : MonoBehaviour {

	private Dictionary<string,int> goods;

	private List<Vertex> buildingLocations;
	private List<Vertex> huts;
	private Vertex campfireVertex;

	private List<SmolMan> freeBois;
	private List<SmolMan> busyBois;

	// Use this for initialization
	void Start () {
		if (buildingLocations == null) {
			buildingLocations = new List<Vertex> ();
		}
		freeBois = new List<SmolMan>();
		busyBois = new List<SmolMan>();
		if (huts == null) {
			huts = new List<Vertex> ();
		}
		goods = new Dictionary<string, int> ();
		SphereTerrain terrain = FindObjectOfType<SphereTerrain> ();
		setCampfireVertex (terrain.getVertex (terrain.findIndexOfNearest (gameObject.transform.position)));
		AddBois(5);
		//TODO: make the bois
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Y)) {
			AddBois(5);
		}
	}

	public void setCampfireVertex(Vertex v) {
		campfireVertex = v;
		addBuilding (v);
	}

	public Vertex getCampfireVertex() {
		return campfireVertex;
	}

	public void addHut(Vertex v) {
		huts.Add (v);
		buildingLocations.Add (v);
	}

	public List<Vertex> getHuts() {
		return huts;
	}

	public List<Vertex> getBuildingLocations() {
		return buildingLocations;
	}

	public void addBuilding(Vertex v) {
		buildingLocations.Add (v);
	}

	public void AddBois(int toAdd){
		toAdd = Mathf.Abs (toAdd);
		for (int i = 0; i < toAdd; i++) {
			GameObject boi = Resources.Load("Prefabs/Person" + Random.Range(0, 8), typeof(GameObject)) as GameObject;
			boi = Instantiate(boi, transform.position, Quaternion.identity);
			boi.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
			boi.GetComponent<SmolMan>().findNewBuilding();
			freeBois.Add(boi.GetComponent<SmolMan>());
		}
	}

	public List<SmolMan> GetFreeBois(){
		return freeBois;
	}

	public bool MakeBusyBoi(){
		Debug.Log("Making busy boi");
		if (IsThereAFreeBoi()) {
			SmolMan becomingBusy = freeBois[0];
			busyBois.Add(becomingBusy);
			freeBois.RemoveAt(0);
			return true;
		}
		return false;
	}

	public List<SmolMan> GetBusyBois(){
		return busyBois;
	}

	public bool IsThereAFreeBoi(){
		return freeBois.Count > 0;
	}

	public void FreeBois(){
		foreach (SmolMan m in busyBois) {
			m.findNewBuilding();
			freeBois.Add(m);
		}
		busyBois.Clear();
	}

	public void SendBoiToGood(string g, Vertex res) {
		if (IsThereAFreeBoi()) {
			Vertex[] resNeighbors = res.getNeighbors();
			for (int i = 0; i < resNeighbors.Length; i++) {
				if (resNeighbors[i].getTransversable()) {
					freeBois[0].setResource(resNeighbors[i]);
					MakeBusyBoi();
					AddGoods(g, 1);
					GetComponent<TierController>().CheckTier();
					break;
				}
			}
		}
	}

	/// <summary>
	/// Adds the goods.
	/// </summary>
	/// <returns><c>true</c>, if goods was added, <c>false</c> otherwise.</returns>
	/// <param name="good">Good.</param>
	/// <param name="amountToAdd">Amount to add.</param>
	public bool AddGoods(string good, int amountToAdd){
		if (goods.ContainsKey (good)) {
			goods[good] = goods [good] + amountToAdd;
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
	public bool RemoveGoods(string good, int amountToRemove){
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
	public bool HasGoodsCheck(string good, int amount){
		if (goods.ContainsKey (good) && goods [good] - amount >= 0)
			return true;
		return false;
	}

	public bool ContainsKey(string good){
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
