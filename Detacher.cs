using UnityEngine;
using System.Collections;

public class Detacher : MonoBehaviour {
		
	void Start () {
		this.transform.DetachChildren ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
