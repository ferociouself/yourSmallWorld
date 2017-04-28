using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEraText : MonoBehaviour {

	public Button mold;
	public Button place;
	public Image info;

	public Button ret;

	bool active;

	public bool firstTimeInTier;
	private float flashTimer;

	bool firstEver;
	private float firstEverTimer;

	// Use this for initialization
	void Start () {
		if(mold == null) mold = GameObject.Find ("Mold").GetComponent<Button>() as Button;
		if(place == null) place = GameObject.Find ("Place").GetComponent<Button>() as Button;
		info.gameObject.SetActive (false);
		active = false;
		firstTimeInTier = true;
		flashTimer = 0.0f;
		firstEverTimer = 0.0f;
		firstEver = true;
	}

	
	// Update is called once per frame
	void Update () {
		if (!MusicController.introduction && firstTimeInTier) {
			flashTimer += Time.deltaTime;
			if (flashTimer >= 1.0f) {
				flashTimer = 0.0f;
				this.gameObject.GetComponent<Text> ().color = (this.gameObject.GetComponent<Text> ().color == Color.red) ? Color.white : Color.red;
			}
			if (firstEver) {
				firstEverTimer += Time.deltaTime;
				if (firstEverTimer > 7) {
					Press ();
				}
			}
		}
	}

	public void Press(){
		mold.interactable = !mold.interactable;
		place.interactable = !place.interactable;
		active = !active;
		info.gameObject.SetActive (active);
		firstTimeInTier = false;
	}

	public void Hover(){
		if(!MusicController.introduction)
		this.gameObject.GetComponent<Text> ().color = ret.colors.highlightedColor;
	}

	public void UnHover(){
		if(!MusicController.introduction)
		this.gameObject.GetComponent<Text> ().color = Color.white;
	}

	public void ChangeEraText(int curTier){
		switch (curTier) {
		case(0):
			this.gameObject.GetComponent<Text>().text = "Prologue";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Hear your name and awaken great one! The world is desolate and harsh, not yet ready for life. Carve out a valley and fill it with water, then plant forests along the banks.";
			break;
		case(1):
			this.gameObject.GetComponent<Text>().text = "The Age of Stone";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Congratulations, your newly arisen people have built themselves a home, taking the first step on the road of civilization. But there is much work to do yet, and many obstacles to overcome. In order to expand, your people need simple huts to live in, a workshop to make primitive stone tools, and a well for a more stable source of water.";
			break;
		case(2):
			this.gameObject.GetComponent<Text> ().text = "The Age of Iron";
			info.transform.GetChild (0).gameObject.GetComponent<Text> ().text = "Their civilization grows, guided by your hand, and the days of foraging for food are now in the past. Your people seek to build a granary to store food in, a water-powered mill for turning their new-found crops into bread, and a smelter to make more advanced, durable tools.";
			break;
		case(3):
			this.gameObject.GetComponent<Text>().text = "The Age of Steel";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Time and technology march forwards. Your people desire to make their mark upon the land and construct even larger buildings, buildings that will stand solid and immovable throughout the ages. They have also invented a method of converting iron into steel, and need a building to perfect this process.";
			break;
		case(4):
			this.gameObject.GetComponent<Text>().text = "The Age of Enlightenment";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "A new birth of culture and scientific advancement! Your people look to the world around them and to the stars above for answers to the mysteries of the universe. Help them to construct a grand institution of learning, a small city unto itself, requiring food and glass equipment. They also want an observatory, to help them gaze upon worlds past their own.";
			break;
		case(5):
			this.gameObject.GetComponent<Text>().text = "The Age of Industry";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "The recent scientific advances made have propelled your people into an era of incredible productivity and growth. They have developed a method of generating electricity using steam, turbines, and special wiring, and so have plans for a building which can produce an incredible amount of this electricity for the whole city. This electricity can then be used to power the urban housing developments they have designed to hold their ever increasing population, which need heating, sewage systems, and lighting.";
			break;
		case(6):
			this.gameObject.GetComponent<Text>().text = "The Age of Technology";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Growth continues, but your people have focused on refining their city and growing more carefully. They have learned to make a more powerful energy source from oil, but in order to generate this new fuel require a gasoline refinery. They have also have refocused their energy into science, and want to build a new, state-of-the-art  lab, with which to further explore the wonders of the natural world. This will require advanced electronic scientific equipment and a strong power source.";
			break;
		case(7):
			this.gameObject.GetComponent<Text>().text = "Ad Caelum Deo";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "The city has reached its zenith, but your people continue to grow and reach ever further, ever upwards, and finally towards the stars. They must leave this small world, and they will need your help now more than ever. This is their most ambitious project yet: to push out into the unknown frontiers of space. They must build a vessel and prepare for the journey, taking as much from their planet as it has to offer. Bestow upon them Deiton, your essence and empower them; the ultimate journey awaits.";
			break;
		case(8):
			this.gameObject.GetComponent<Text>().text = "Epilogue";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Your people have left their home, and so have left you. You raised and molded them as best you could, and look at them now! Flying off to make their own destiny, to make their own mark upon the heavens. You have done well. And do not worry; they are not truly alone: you gave of yourself, and so a piece of you will be with them always. Will be with us always. We have traveled the stars, discovered wonders, built marvels, and all because of you.  But now, great one, rest your eyes. And thank you. We will always remember you.\n\nRequiescat in Pace et in Amore.";
			break;
		default:
			this.gameObject.GetComponent<Text>().text = "ERA UNKNOWN";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "I have nothing left for you.";
			break;
		}
	}
}
