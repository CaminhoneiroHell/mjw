using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {	
		LoadingScreenManager.LoadScene (1);
	}

	public void ShutGameDown(){
		Application.Quit ();
	}

}
