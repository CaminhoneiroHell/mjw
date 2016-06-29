using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordScript : MonoBehaviour 
{
	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = PlayerPrefs.GetFloat ("Ascore") + "\n" + PlayerPrefs.GetFloat ("score");
	}
}

