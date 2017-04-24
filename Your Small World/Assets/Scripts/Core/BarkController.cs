using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkController : MonoBehaviour {

	public float timer;
	private const float minWaitTime = 5.0f;
	private const float swingWaitTime = 40.0f;
	public float timeToWait;

	private const int dieRollHelp = 4;

	List<string> waterBarks;
	List<string> stoneBarks;
	List<string> sandBarks;
	List<string> treeBarks;
	List<string> wheatBarks;
	List<string> oilBarks;
	List<string> ironBarks;
	List<string> copperBarks;
	List<string> coalBarks;
	List<string> deitonBarks;
	List<string> altBarks;

	List<string> choices;

	public bool barking;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		timeToWait = minWaitTime + Random.Range (0, swingWaitTime);
		InitLists ();
		FillLists ();
		this.gameObject.GetComponent<TextMesh> ().text = "";
		barking = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timeToWait) {
			Decide ();
		}
		if (barking && timer >= minWaitTime) {
			barking = false;
			this.gameObject.GetComponent<TextMesh> ().text = "";
		}
	}

	void Decide(){
		timer = 0.0f;
		timeToWait = minWaitTime + Random.Range (0, swingWaitTime);
		if (GameObject.FindObjectOfType<Community> () != null) {
			bool help = (Random.Range (0, dieRollHelp) == 0);
			TierController tier = GameObject.FindObjectOfType<TierController> ();
			string woof = "";
			barking = true;
			if (help) {
				choices.Clear ();
				if (tier.CheckIfWant ("Water")) {
					foreach (string s in waterBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Stone")) {
					foreach (string s in stoneBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Sand")) {
					foreach (string s in sandBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Tree")) {
					foreach (string s in treeBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Wheat")) {
					foreach (string s in wheatBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Oil")) {
					foreach (string s in oilBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Iron")) {
					foreach (string s in ironBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Copper")) {
					foreach (string s in copperBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Coal")) {
					foreach (string s in coalBarks) {
						choices.Add (s);
					}
				}
				if (tier.CheckIfWant ("Deiton")) {
					foreach (string s in deitonBarks) {
						choices.Add (s);
					}
				}
				int decision = Random.Range (0, choices.Count);
				woof = choices [decision];
			} else {
				int decisionMeaningless = Random.Range (0, altBarks.Count);
				woof = altBarks [decisionMeaningless];
			}
			this.gameObject.GetComponent<TextMesh> ().text = woof;
		}
	}

	void InitLists(){
		waterBarks = new List<string>();
		stoneBarks = new List<string>();
		sandBarks = new List<string>();
		treeBarks = new List<string>();
		wheatBarks = new List<string>();
		oilBarks = new List<string>();
		ironBarks = new List<string>();
		copperBarks = new List<string>();
		coalBarks = new List<string>();
		deitonBarks = new List<string>();
		altBarks = new List<string>();

		choices = new List<string> ();
	}

	void FillLists(){
		waterBarks.Add ("aquam requiramos");
		stoneBarks.Add("calcem requiramos");
		sandBarks.Add("pulverem speculum requiramos");
		treeBarks.Add("silvam requiramos");
		wheatBarks.Add("granum requiramos");
		oilBarks.Add("oleum requiramos");
		ironBarks.Add("ferrum requiramos");
		copperBarks.Add("aenum requiramos");
		coalBarks.Add("carbonem requiramos");
		deitonBarks.Add("deiton requiramos");
		altBarks.Add("deus vult");
		altBarks.Add("tibi ludum daremus");
		altBarks.Add("mundus mirabilis!");
		altBarks.Add("langueo...");
		altBarks.Add("salve!");
		altBarks.Add("eheu! mea brassica!");
		altBarks.Add("te laudamos");
		altBarks.Add("Heus!");
		altBarks.Add("quid es teum nomen?");
		altBarks.Add("quid agis?");
		altBarks.Add("quid novi?");
		altBarks.Add("intereo...");
		altBarks.Add("ignosce...");
		altBarks.Add("ut vales?");
		altBarks.Add("gratias");
		altBarks.Add("salutatio!");
		altBarks.Add("bene, bene...");
		altBarks.Add("et tu brute?");
		altBarks.Add ("");
		altBarks.Add ("");
		altBarks.Add ("");
		altBarks.Add ("");
	}
}
