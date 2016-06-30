using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public float timeToDie;
	void Update () {
		Destroy (this.gameObject, timeToDie);
	}
}
