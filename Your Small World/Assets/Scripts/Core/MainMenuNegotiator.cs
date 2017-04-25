using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MainMenuNegotiator : MonoBehaviour {

	public Button begin;
	public Button credits;
	public Button back;

	public Image img;
	public Text creditsText;

	AudioMixer AM;

	bool ending;
	float timer;

	// Use this for initialization
	void Start () {
		ending = false;
		timer = 0.0f;
		AM = Resources.Load ("MasterMixer") as AudioMixer;
		this.gameObject.GetComponent<AudioSource> ().PlayDelayed (1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (ending) {
			timer += Time.deltaTime;
			AM.SetFloat ("MainMenuVolume", (float)Mathf.Lerp (0.00f, -40.00f, timer / 2.00f));
			if (timer >= 2.00f) {
				UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (1);
			}
		}

	}

	public void Proceed(){
		ending = true;
	}

	public void Show(){
		begin.gameObject.SetActive (false);
		credits.gameObject.SetActive (false);
		back.gameObject.SetActive (true);
		img.gameObject.SetActive (false);
		creditsText.gameObject.SetActive (true);
	}

	public void Hide(){
		begin.gameObject.SetActive (true);
		credits.gameObject.SetActive (true);
		back.gameObject.SetActive (false);
		img.gameObject.SetActive (true);
		creditsText.gameObject.SetActive (false);
	}
}
