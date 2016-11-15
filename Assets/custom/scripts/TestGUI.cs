using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestGUI : MonoBehaviour {
	public string level2Load;

	// Use this for initialization
	void Start () {
		/*
		handButtonImg = GameObject.Find(gameObject.name+"/handButton").GetComponent<RawImage> ();
		handButton = GameObject.Find(gameObject.name+"/handButton").GetComponent<Selectable> ();
		eventListener = GameObject.Find(gameObject.name+"/EventSystem").GetComponent<EventSystem> ();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (handButton.OnSelect(eventListener))
			Debug.Log ("handButton Selected!");
		*/

	}
	public void imageClicked(){
		Debug.Log ("Clicked on me:"+gameObject.name);
		Application.LoadLevel (level2Load);
	}
}
