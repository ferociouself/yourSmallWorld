using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEraText : MonoBehaviour {

	Button mold;
	Button place;
	public Image info;

	public Button ret;

	bool active;

	// Use this for initialization
	void Start () {
		mold = GameObject.Find ("Mold").GetComponent<Button>() as Button;
		place = GameObject.Find ("Place").GetComponent<Button>() as Button;
		info.gameObject.SetActive (false);
		active = false;
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void Press(){
		mold.interactable = !mold.interactable;
		place.interactable = !place.interactable;
		active = !active;
		info.gameObject.SetActive (active);
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
			this.gameObject.GetComponent<Text>().text = "Prologue - An Awakening";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Hear your name and awaken great one! The world is desolate and harsh, and your people need your help to survive. The first thing they need is a source of water and a place to hunt.";
			break;
		case(1):
			this.gameObject.GetComponent<Text>().text = "An Age of Stone";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Congratulations, your people have taken the first step on the road of civilization. But there is much work to do yet, and many obstacles to overcome. In order to expand, your people need simple huts to live in, a workshop to make primitive stone tools, and a well for a more stable source of water.";
			break;
		case(2):
			this.gameObject.GetComponent<Text> ().text = "An Age of Iron";
			info.transform.GetChild (0).gameObject.GetComponent<Text> ().text = "Their civilization grows, guided by your hand, and the days of foraging for food are now in the past. Your people seek to build a granary to store food in, a water-powered mill for turning their new-found crops into bread, and a smelter to make more advanced, durable tools.";
			break;
		case(3):
			this.gameObject.GetComponent<Text>().text = "An Age of Steel";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Time and technology march forwards. Your people desire to make their mark upon the land and construct even larger buildings, buildings that will stand solid and immovable throughout the ages. They have also invented a method of converting iron into steel, and need a building to perfect this process.";
			break;
		case(4):
			this.gameObject.GetComponent<Text>().text = "An Age of Enlightenment";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "A new birth of culture and scientific advancement! Your people look to the world around them and to the stars above for answers to the mysteries of the universe. Help them to construct a grand institution of learning, a small city unto itself, requiring food and glass equipment. They also want an observatory, to help them gaze upon worlds past their own.";
			break;
		case(5):
			this.gameObject.GetComponent<Text>().text = "An Age of Industry";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "The recent scientific advances made have propelled your people into an era of incredible productivity and growth. They have developed a method of generating electricity using steam, turbines, and special wiring, and so have plans for a building which can produce an incredible amount of this electricity for the whole city. This electricity can then be used to power the urban housing developments they have designed to hold their ever increasing population, which need heating, sewage systems, and lighting.";
			break;
		case(6):
			this.gameObject.GetComponent<Text>().text = "An Age of Technology";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Growth continues, but your people have focused on refining their city and growing more carefully. They have learned to make a more powerful energy source from oil, but in order to generate this new fuel require a gasoline refinery. They have also have refocused their energy into science, and want to build a new, state-of-the-art  lab, with which to further explore the wonders of the natural world. This will require advanced electronic scientific equipment and a strong power source.";
			break;
		case(7):
			this.gameObject.GetComponent<Text>().text = "Ad Caelum Deo";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "The city has reached its zenith, but your people continue to grow and reach ever further, ever upwards, and finally towards the stars. They must leave this small world, and they will need your help now more than ever. This is their most ambitious project yet: to push out into the unknown frontiers of space. They must build a vessel and prepare for the journey, taking as much from their planet as it has to offer. Bestow upon them Deiton, your essence and empower them; the ultimate journey awaits.";
			break;
		case(8):
			this.gameObject.GetComponent<Text>().text = "Epilogue - Sleep";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "We raise our children to leave. You molded them as best you could, and look at them now! Flying off to seek their own fate, to make their own mark upon the heavens. You have done well. Do not worry: you gave of yourself, and so a piece of you will be with them always. But now, great one, rest your eyes. And thank you. We will always remember you.";
			break;
		default:
			this.gameObject.GetComponent<Text>().text = "ERA UNKNOWN";
			info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "I have nothing left for you.";
			break;
		}
	}
}
