using UnityEngine;
using System.Collections;

public class FrontMenu : MonoBehaviour 
{
	Animator animator;
	public static bool beginGame;
	public GameObject menuScene;
	public GameObject gameScene;
	public GameObject mainCamera;
	public GameObject resetScoreBtn;


	void Start()
	{   
		animator = GetComponent<Animator> ();
	}

	void Update () 
	{
		//Event Business
		if(beginGame)
		{
			animator.SetBool("Fly", true);
			PlayerPrefs.SetFloat("Ascore", 0f);
			StartCoroutine ("Mapchange");
		}
		else
		{
			animator.SetBool("Fly", false);
		}
	}

	public void beginGameHelper(bool kfc)
	{
		beginGame = kfc;
	}


	public IEnumerator Mapchange()
	{
		yield return new WaitForSeconds(3);
		if (menuScene != null) {
			gameScene.SetActive(true);
			mainCamera.GetComponent<Camera2DFollow> ().enabled = true;
			Destroy (menuScene);
			resetScoreBtn.SetActive (false);
			Destroy (this);
		}
	}


}
