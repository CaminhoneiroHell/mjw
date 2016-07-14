using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	public string[] hintList;
	public Text hintPopup;
	public GameObject playeRef;
	public GameObject howToPlayRef;
	public GameObject quitGmBtnRef;
	public Transform target;
	public Transform nextTarget;
	public Transform previousTarget;

	#region Camera Follow Variables
//	public float damping = 1;
//	public float lookAheadFactor = 3;
//	public float lookAheadReturnSpeed = 0.5f;
//	public float lookAheadMoveThreshold = 0.1f;
//	float offsetZ;
//	Vector3 lastTargetPosition;
//	Vector3 currentVelocity;
//	Vector3 lookAheadPos;
//	bool inCameraView;
	#endregion

	void Start () 
    {
//		lastTargetPosition = target.position;
//		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
		howToPlayRef.SetActive (true);
		quitGmBtnRef.SetActive (false);
	}

	public void PlayShake() {
		StopAllCoroutines();
		StartCoroutine("Shake");
	}



	public void ChangeTarget()
	{
		if (previousTarget = target)
		target = nextTarget;
		nextTarget = previousTarget;
	}

	#region CameraFollow
//	void FollowTargetComponent()
//	{
//		if (target != null) {
//			float xMoveDelta = (target.position - lastTargetPosition).x;
//
//			bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;
//
//			if (updateLookAheadTarget) {
//				lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
//			} else {
//				lookAheadPos = Vector3.MoveTowards (lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
//			}
//
//			Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
//			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref currentVelocity, damping);
//
//			transform.position = newPos;
//			lastTargetPosition = target.position;		
//		}
//	}
	#endregion

	void Update () {
		if (playeRef == null) {
			LevelManager ();
		}

        if (Time.frameCount % 30 == 0)
        {
            System.GC.Collect();
        }
    }

	public float duration = 0.5f;
	public float magnitude = 0.1f;

	IEnumerator Shake() {

		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;    

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}

	void LevelManager()
	{
		StartCoroutine ("LoadLevel");
	}

	public IEnumerator LoadLevel()
	{
		yield return new WaitForSeconds (1);
		Application.LoadLevel (Application.loadedLevel);
//		LoadingScreenManager.LoadScene (1);
	}

	int index;
	public void WriteHint()
	{
		hintPopup.gameObject.SetActive (true);
		index = Random.Range (0,hintList.Length);
		hintPopup.text = hintList [index];
		Destroy (howToPlayRef, 3f);
	}
}
