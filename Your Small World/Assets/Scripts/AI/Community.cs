using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Community : MonoBehaviour {

	private Dictionary<BaseResource,int> goods;

	// Use this for initialization
	void Start () {
		goods = new Dictionary<BaseResource, int> ();
	}
	
	// Update is called once per frame
	void Update () {
		
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

}
