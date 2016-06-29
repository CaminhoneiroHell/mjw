using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

	public Transform target;
	public Transform nextTarget;
	public Transform previousTarget;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;
	bool inCameraView;

//	public Bounds bounds = new Bounds(Vector3.zero, new Vector3(1, 2, 1));

	void Start () 
    {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
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

	void Update () {
		
		if (target != null) {
			float xMoveDelta = (target.position - lastTargetPosition).x;

			bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;

			if (updateLookAheadTarget) {
				lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
				} else {
				lookAheadPos = Vector3.MoveTowards (lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
			}

			Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref currentVelocity, damping);

			transform.position = newPos;
			lastTargetPosition = target.position;		
		}

		if (target == null) {
			LevelManager ();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			ChangeTarget();
		}
	}

	public float duration = 0.5f;
//	public float speed = 1.0f;
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
	}
}
