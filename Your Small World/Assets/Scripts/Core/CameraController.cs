using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	const float originZoomAmt = 20.0f;
	const float percentTimeBetween = 0.25f;
	float zoomAmt = originZoomAmt;

	Vector3 lastMousePos;

	public float timer;

	public Text LatinText;
	public Text EnglishText;
	public Text TitleText;
	public int stage;

	float fadeTimer;

	public float finalTimer;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		fadeTimer = 0.0f;
		finalTimer = 0.0f;
		Vector3 awayFromSphere = Camera.main.transform.position - gameObject.transform.position;
		Camera.main.transform.position = awayFromSphere.normalized * originZoomAmt*3 + gameObject.transform.position;
		stage = 0;
		if (LatinText != null) {
			LatinText.text = "Principio magni speciem glomeravit in orbis.\nTum freta diffundi rapidisque tumescere ventis.";
			LatinText.color = new Color(255,255,255,1); //black
		}
		if (EnglishText != null) {
			EnglishText.text = "First he gathered up the land into the shape of a great orb.\nThen he ordered the seas to spread and rise in the rushing winds.";
			EnglishText.color = new Color(0,0,0,0); //black alpha'ed
		}
	}

	// Update is called once per frame
	void Update () {
		if (!MusicController.introduction) {
			if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
				//Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 
				//		Mathf.Max(-20.0f, Mathf.Min(-7.0f, Camera.main.transform.position.z + Input.GetAxis("Mouse ScrollWheel"))));

				zoomAmt = Mathf.Min (20.0f, Mathf.Max (7.0f, zoomAmt - Input.GetAxis ("Mouse ScrollWheel")));

				Vector3 awayFromSphere = Camera.main.transform.position - gameObject.transform.position;
				Camera.main.transform.position = awayFromSphere.normalized * zoomAmt + gameObject.transform.position;
			}
			if (stage < 4) {
				stage = 4;
				TitleText.text = "Ludum Dei";
				TitleText.fontSize = 80;
				TitleText.color = Color.black;
			}
			finalTimer += Time.deltaTime;
			if (finalTimer >= 3.0f) {
				TitleText.color = new Color(TitleText.color.r, TitleText.color.g, TitleText.color.b, (float)Mathf.Lerp(1,0,finalTimer/5.0f));
			}
		} else {
			float dT = Time.deltaTime;
			timer += dT;
			fadeTimer += dT;
			zoomAmt = Mathf.Lerp (originZoomAmt*3, 10.0f, (float)(timer / MusicController.introClip.length));
			Vector3 awayFromSphere = Camera.main.transform.position - gameObject.transform.position;
			Camera.main.transform.position = awayFromSphere.normalized * zoomAmt + gameObject.transform.position;

			if (stage == 0 && timer / MusicController.introClip.length >= percentTimeBetween) {
				stage = 1;
				fadeTimer = 0;
			} else if (stage == 1 && timer / MusicController.introClip.length >= percentTimeBetween*2) {
				stage = 2;
				fadeTimer = 0;
			} else if (stage == 2 && timer / MusicController.introClip.length >= percentTimeBetween*3) {
				stage = 3;
				fadeTimer = 0;
			} else if (stage == 3 && timer / MusicController.introClip.length >= 1.0) {
				stage = 4;
				fadeTimer = 0;
			}


			switch (stage) {
			case(0): //fade out latin 0, fade in english 0
				LatinText.color = Color.white;
				LatinText.color = new Color (LatinText.color.r, LatinText.color.g, LatinText.color.b, (float)(1 - (fadeTimer+0.5) / (percentTimeBetween * MusicController.introClip.length)));
				EnglishText.color = new Color (EnglishText.color.r, EnglishText.color.g, EnglishText.color.b, Mathf.Max(0.05f,(float)(fadeTimer / (percentTimeBetween * MusicController.introClip.length))));
				break;
			case(1): //fade out english 0
				EnglishText.color = Color.black;
				EnglishText.color = new Color (EnglishText.color.r, EnglishText.color.g, EnglishText.color.b, (float)(1 - fadeTimer / (percentTimeBetween * MusicController.introClip.length)));
				break;
			case(2): //fade out latin 1, fade in english 1
				LatinText.text = "Iussit et extendi campos, subsidere valles,\nFronde tegi silvas, lapidosos surgere montes.";
				EnglishText.text = "He ordered the plains to extend, the valleys to subside,\nthe leaves to hide the trees, and the stony mountains to rise.";
				LatinText.color = Color.white;
				LatinText.color = new Color (LatinText.color.r, LatinText.color.g, LatinText.color.b, (float)(1 - (fadeTimer+0.5) / (percentTimeBetween * MusicController.introClip.length)));
				EnglishText.color = new Color (EnglishText.color.r, EnglishText.color.g, EnglishText.color.b, Mathf.Max(0.05f,(float)(fadeTimer / (percentTimeBetween * MusicController.introClip.length))));

				break;
			case(3): //fade out english 1
				EnglishText.color = Color.black;
				EnglishText.color = new Color (EnglishText.color.r, EnglishText.color.g, EnglishText.color.b, (float)(1 - fadeTimer / (percentTimeBetween * MusicController.introClip.length)));
				break;
			case(4): //display title
				TitleText.text = "Ludum Dei";
				TitleText.fontSize = 80;
				TitleText.color = Color.black;
				break;
			default:
				break;
			}
		}
	}

	IEnumerator OnMouseDown() {
		lastMousePos = Input.mousePosition;

		yield return null;
	}

	IEnumerator OnMouseDrag() {

		Vector3 curMousePos = Input.mousePosition;

		Vector3 mouseDelta = curMousePos - lastMousePos;

		Transform child = transform.GetChild(0).GetChild(0);

		child.rotation = child.rotation * Quaternion.Euler(-mouseDelta.y * (zoomAmt / 100.0f), mouseDelta.x * (zoomAmt / 100.0f), 0.0f);

		lastMousePos = curMousePos;
		yield return null;
	}

	IEnumerator OnMouseUp() {
		lastMousePos = Vector3.zero;

		yield return null;
	}
}
