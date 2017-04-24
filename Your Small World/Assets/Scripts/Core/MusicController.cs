using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

	AudioMixer AM;

	//Audiosources
	AudioSource intro;
	AudioSource loop1;
	AudioSource loop2;
	AudioSource drums;
	AudioSource pedals;
	AudioSource paint1;
	AudioSource paint2;
	AudioSource paint3;
	AudioSource paint4;

	//datastructure(s)
	List<AudioClip> loops;
	List<AudioClip> tones;

	//Audioclips
	public static AudioClip introClip;
	AudioClip loop1Clip;
	AudioClip loop2Clip;
	AudioClip loop3Clip;
	AudioClip loop4Clip;
	AudioClip loop5Clip;
	AudioClip loop6Clip;
	AudioClip loop7Clip;
	AudioClip loop8Clip;
	AudioClip loop9Clip;
	AudioClip loop10Clip;
	AudioClip loop11Clip;
	AudioClip loop12Clip;
	AudioClip loop13Clip;
	AudioClip drumClip;
	AudioClip pedal1Clip;
	AudioClip pedal2Clip;
	AudioClip tone1;
	AudioClip tone2;
	AudioClip tone3;
	AudioClip tone4;
	AudioClip tone5;
	AudioClip tone6;
	AudioClip tone7;
	AudioClip tone8;

	//LogicElements
	public static bool introduction;
	double timer;
	double introTimer;
	bool firstLoopIsPlaying;
	double loopTimer;
	double pedalTimer;

	//special logic elements
	float introlowpasscufoffmin;
	float introlowpasscufoffmax;
	bool placing;
	bool fading;
	float fadeTimer;
	int placingTonePos;
	double placingTimer;
	const float placeToneIncrementer = 2; //in seconds
	const float maxFXVOL = 3.0f; //in decibles

	// Use this for initialization
	void Start () {
		timer = 0.0;
		AM = Resources.Load ("MasterMixer") as AudioMixer;

		loop1 = new AudioSource ();
		loop2 = new AudioSource ();
		drums = new AudioSource ();
		pedals = new AudioSource ();
		paint1 = new AudioSource();
		paint2 = new AudioSource();
		paint3 = new AudioSource();
		paint4 = new AudioSource();

		intro = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		loop1 = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		loop2 = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		drums = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		pedals = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		paint1 = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		paint2 = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		paint3 = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;
		paint4 = this.gameObject.AddComponent (typeof(AudioSource)) as AudioSource;

		if (AM != null) {
			intro.outputAudioMixerGroup = AM.FindMatchingGroups ("Intro") [0];
			AudioMixerGroup looper = AM.FindMatchingGroups ("Loops") [0];
			loop1.outputAudioMixerGroup = looper;
			loop2.outputAudioMixerGroup = looper;
			drums.outputAudioMixerGroup = AM.FindMatchingGroups ("Drums") [0];
			pedals.outputAudioMixerGroup = AM.FindMatchingGroups ("Pedals") [0];
			AudioMixerGroup FXer = AM.FindMatchingGroups ("FX") [0];
			paint1.outputAudioMixerGroup = FXer;
			paint2.outputAudioMixerGroup = FXer;
			paint3.outputAudioMixerGroup = FXer;
			paint4.outputAudioMixerGroup = FXer;
		}

		loops = new List<AudioClip> ();
		tones = new List<AudioClip> ();

		introClip = Resources.Load("Audio/loops/ClayOpen") as AudioClip;
		loop1Clip = Resources.Load("Audio/loops/ClayLoop1") as AudioClip;
		loop2Clip = Resources.Load("Audio/loops/ClayLoop2") as AudioClip;
		loop3Clip = Resources.Load("Audio/loops/ClayLoop3") as AudioClip;
		loop4Clip = Resources.Load("Audio/loops/ClayLoop4") as AudioClip;
		loop5Clip = Resources.Load("Audio/loops/ClayLoop5") as AudioClip;
		loop6Clip = Resources.Load("Audio/loops/ClayLoop6") as AudioClip;
		loop7Clip = Resources.Load("Audio/loops/ClayLoop7") as AudioClip;
		loop8Clip = Resources.Load("Audio/loops/ClayLoop8") as AudioClip;
		loop9Clip = Resources.Load("Audio/loops/ClayLoop9") as AudioClip;
		loop10Clip = Resources.Load("Audio/loops/ClayLoop10") as AudioClip;
		loop11Clip = Resources.Load("Audio/loops/ClayLoop11") as AudioClip;
		loop12Clip = Resources.Load("Audio/loops/ClayLoop12") as AudioClip;
		loop13Clip = Resources.Load("Audio/loops/ClayLoop13") as AudioClip;
		drumClip = Resources.Load("Audio/ClayDrumLop") as AudioClip;
		pedal1Clip = Resources.Load("Audio/ClayPed1234") as AudioClip;
		pedal2Clip = Resources.Load("Audio/ClayPed1235") as AudioClip;
		tone1 = Resources.Load("Audio/scale/1") as AudioClip;
		tone2 = Resources.Load("Audio/scale/2") as AudioClip;
		tone3 = Resources.Load("Audio/scale/3") as AudioClip;
		tone4 = Resources.Load("Audio/scale/4") as AudioClip;
		tone5 = Resources.Load("Audio/scale/5") as AudioClip;
		tone6 = Resources.Load("Audio/scale/6") as AudioClip;
		tone7 = Resources.Load("Audio/scale/7") as AudioClip;
		tone8 = Resources.Load("Audio/scale/8") as AudioClip;

		loops.Add (loop1Clip);
		loops.Add (loop2Clip);
		loops.Add (loop3Clip);
		loops.Add (loop4Clip);
		loops.Add (loop5Clip);
		loops.Add (loop6Clip);
		loops.Add (loop7Clip);
		loops.Add (loop8Clip);
		loops.Add (loop9Clip);
		loops.Add (loop10Clip);
		loops.Add (loop11Clip);
		loops.Add (loop12Clip);
		loops.Add (loop13Clip);

		tones.Add (tone1);
		tones.Add (tone2);
		tones.Add (tone3);
		tones.Add (tone4);
		tones.Add (tone5);
		tones.Add (tone6);
		tones.Add (tone7);
		tones.Add (tone8);

		introduction = true;
		introTimer = introClip.length;
		intro.clip = introClip;
		intro.Play ();

		loopTimer = 0.0;
		firstLoopIsPlaying = false;
		loop1.clip = loop3Clip;

		drums.clip = drumClip;

		pedalTimer = 0.0;
		pedals.clip = pedal2Clip;

		introlowpasscufoffmin = 0.00f;
		introlowpasscufoffmax = 22000.00f;
		AM.SetFloat ("IntroLowPassCutoff", introlowpasscufoffmin);

		placing = false;
		fading = false;
		fadeTimer = 0.0f;
		placingTonePos = 0;
		placingTimer = 0.0;
	}
	
	// Update is called once per frame
	void Update () {
		double dT = Time.deltaTime;
		timer += dT;
		introTimer -= dT;
		loopTimer -= dT;
		pedalTimer -= dT;

		if (introduction && introTimer <= 0.0) {
			introduction = false;
			drums.Play ();
		}

		if (!introduction) {
			if (pedalTimer <= 0.0) {
				AlternatePedals ();
			}
			if (loopTimer <= 0.0) {
				CrossNewLoop ();
			}
		} 
		AM.SetFloat("IntroLowPassCutoff", (float)Mathf.Lerp (introlowpasscufoffmin, introlowpasscufoffmax, (float)(introClip.length - introTimer) / introClip.length));

		if (placing) Placeing ();
		if (fading){
			fadeTimer += Time.deltaTime;
			AM.SetFloat ("FXVolume", Mathf.Lerp (maxFXVOL, -70.0f, (float)(fadeTimer / 8.0)));
			float value = 1.0f;
			if (AM.GetFloat ("FXVolume", out value) && value <= -50.0f){ 
				StopPlacing ();
			}
		}
	}

	void OnGUI(){
		/*if (GUI.Button (new Rect (10, 10, 100, 20), "Start Placing")) {
			StartPlacing ();
		}
		if (GUI.Button (new Rect (10, 40, 100, 20), "Stop Placing")) {
			fadeTimer = 0.0f;
			fading = true;
		}
		if (GUI.Button (new Rect (10, 70, 100, 20), "Tap Place")) {
			PlaceSingle ();
		}*/
	}

	void AlternatePedals(){
		if (pedals.clip == pedal1Clip) {
			pedals.clip = pedal2Clip;
			pedalTimer = pedal2Clip.length;
		} else {
			pedals.clip = pedal1Clip;
			pedalTimer = pedal1Clip.length;
		}
		pedals.Play ();
		drums.time = 0.0f;
		if (CameraController.finalTimer >= 3.0f && Random.Range (0, 20) == 0) {
			drums.Stop ();
		} else {
			drums.Play ();
		}
	}

	void CrossNewLoop(){
		if (firstLoopIsPlaying) { //play 2 and load 1
			loopTimer = loop2.clip.length;
			loop2.Play ();

			loop1.clip = DecideLoop ();

		} else { //play 1 and load 2
			loopTimer = loop1.clip.length;
			loop1.Play ();

			loop2.clip = DecideLoop ();
		}
		firstLoopIsPlaying = !firstLoopIsPlaying;
	}

	AudioClip DecideLoop(){
		return loops[Random.Range (0, 13)];
	}

	public void StartPlacing(){
		if (!placing) {
			placing = true;
			fading = false;
			AM.SetFloat ("FXVolume", maxFXVOL);
			placingTimer = 0.0;
			placingTonePos = Random.Range (0, 4);
			paint1.clip = tones [placingTonePos];
			paint2.clip = tones [placingTonePos + 1];
			paint3.clip = tones [placingTonePos + 2];
			paint4.clip = tones [placingTonePos + 3];
			paint1.loop = true;
			paint2.loop = true;
			paint3.loop = true;
			paint4.loop = true;
			paint1.Play ();
		}
	}

	void Placeing(){
		placingTimer += Time.deltaTime;
		if (placingTimer >= placeToneIncrementer && !paint2.isPlaying) {
			paint2.Play();
		} else if (placingTimer >= 2 * placeToneIncrementer && !paint3.isPlaying) {
			paint3.Play();
		} else if (placingTimer >= 3 * placeToneIncrementer && !paint4.isPlaying) {
			paint4.Play();
		}
	}

	public void StopPlacing(){
		placing = false;
		if (paint1.isPlaying) {
			paint1.Stop ();
			paint1.time = 0.0f;
		}
		if (paint2.isPlaying) {
			paint2.Stop ();
			paint2.time = 0.0f;
		}
		if (paint3.isPlaying) {
			paint3.Stop ();
			paint3.time = 0.0f;
		}
		if (paint4.isPlaying) {
			paint4.Stop ();
			paint4.time = 0.0f;
		}
	}

	public void PlaceSingle(){
		AM.SetFloat ("FXVolume", maxFXVOL);
		fadeTimer = 0.0f;
		placingTonePos = Random.Range (0, 8);
		paint1.clip = tones [placingTonePos];
		paint1.Play ();
		fading = true;
	}

	public void SetFading() {
		fadeTimer = 0.0f;
		fading = true;
	}
		
}
