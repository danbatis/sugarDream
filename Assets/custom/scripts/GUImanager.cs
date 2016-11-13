using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour {
	RawImage backgroundImage;
	Text chapterText;

	public string[] chapters;
	public int chapterIndex=0;

	public float fadeTime = 0.5f;
	private float fadeStart;
	private bool fadein;
	private bool fadeout;

	// Use this for initialization
	void Start () {
		chapterText = GetComponentInChildren<Text> ();
		backgroundImage = GetComponentInChildren<RawImage> ();
		backgroundImage.CrossFadeAlpha (1f,0f,true);
		chapterText.text = chapters [chapterIndex];
		Debug.Log ("game started...");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (chapterIndex == 0) {
				backgroundImage.CrossFadeAlpha (1f,0f,true);
				changeText ();
				fadein = true;
				backgroundImage.CrossFadeAlpha (0f,fadeTime,false);
			} else {
				backgroundImage.CrossFadeAlpha (1f,fadeTime,false);
				fadeout = true;
			}
			fadeStart = Time.time;
		}
		if (fadein) {
			if (Time.time - fadeStart >= fadeTime) {
				fadein = false;
			}
		}
		if (fadeout) {
			if (Time.time - fadeStart >= fadeTime) {
				changeText ();
				fadeout = false;
				fadein = true;
				backgroundImage.CrossFadeAlpha (0f,fadeTime,false);
				fadeStart = Time.time;
			}
		}
	}

	void changeText(){
		chapterIndex += 1;
		if (chapterIndex >= chapters.Length) {
			Debug.Log ("ok, reached the end, for now let us go back to the first chapter");
			chapterIndex = 0;
		}

		chapterText.text = chapters [chapterIndex];
	}
}
