﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour {
	RawImage backgroundImage;
	RawImage storyImage;
	Text chapterText;
	public string nextScene;

	public float fadeTime = 0.5f;
	public int frameTextIndex=0;

	public string[] frameTexts;
	public Texture[] storyImages;
	public int[] storyImageLines;
	private int storyImageIndex=0;

	public AudioClip[] sounds;
	public int[] soundLines;
	private int soundLineIndex=0;


	private float fadeStart;
	private bool fadein;
	private bool fadeout;
	private AudioSource myAudio;

	// Use this for initialization
	void Start () {
		chapterText = GetComponentInChildren<Text> ();
		backgroundImage = GameObject.Find(gameObject.name+"/BlackLayer4fade").GetComponent<RawImage> ();
		storyImage = GameObject.Find(gameObject.name+"/storyImage").GetComponent<RawImage> ();
		myAudio = GetComponent<AudioSource> ();
		backgroundImage.CrossFadeAlpha (1f,0f,true);
		chapterText.text = frameTexts [frameTextIndex];

		if (storyImages.Length == storyImageLines.Length) {
			if (frameTextIndex == 0) {
				Debug.Log ("game started from scratch...");
			}
			else {
				Debug.Log ("game started from some specific point...");
			}
			InitStory ();
		}
		else{
			Debug.Log ("The arrays storyImages and storyImageLines shall have the same length, because one is the specification for what chapter each image is.");
			Application.Quit ();
			Time.timeScale = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (frameTextIndex != 0) {
				backgroundImage.CrossFadeAlpha (1f,fadeTime,false);
				fadeout = true;
				fadeStart = Time.time;
			}
		}
		if (fadein) {
			if (Time.time - fadeStart >= fadeTime) {
				fadein = false;
			}
		}
		if (fadeout) {
			if (Time.time - fadeStart >= fadeTime) {
				nextFrame ();
				fadeout = false;
				fadein = true;
				backgroundImage.CrossFadeAlpha (0f,fadeTime,false);
				fadeStart = Time.time;
			}
		}
	}
	void InitStory(){
		//prepare story image
		int targetFrameText = frameTextIndex+1;
		for (int i = 0; i < storyImageLines.Length; i++) {
			if (storyImageLines [i] >= targetFrameText) {
				storyImageIndex = i;
				break;
			}
		}
		//hide storyImage
		storyImage.enabled = false;
		backgroundImage.CrossFadeAlpha (1f,0f,true);
		nextFrame();
		fadein = true;
		backgroundImage.CrossFadeAlpha (0f,2*fadeTime,false);
		fadeStart = Time.time;
	}
	void nextFrame(){
		//check if there is a story image
		if (storyImageIndex >= 0) {
			if (storyImageLines [storyImageIndex] == frameTextIndex)
				nextImage ();
			else
				nextText ();

			if (soundLines [soundLineIndex] == frameTextIndex) {
				myAudio.PlayOneShot (sounds[soundLineIndex]);
				soundLineIndex += 1;
			}
		} 
		else{
			nextText ();				
		}
	}
	void nextText(){
		storyImage.enabled = false;
		frameTextIndex += 1;
		if (frameTextIndex >= frameTexts.Length) {
			Debug.Log ("ok, reached the end, for now let us go back to the first chapter");
			if (nextScene != "")
				Application.LoadLevel (nextScene);
			frameTextIndex = 0;
		}
		chapterText.text = frameTexts [frameTextIndex];		
	}
	void nextImage(){
		storyImage.enabled = true;
		storyImage.texture = storyImages[storyImageIndex];
		storyImageIndex += 1;
		if (storyImageIndex >= storyImages.Length)
			storyImageIndex = -1;
	}
}
