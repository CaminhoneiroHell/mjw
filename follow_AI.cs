

using UnityEngine;
using System.Collections;

public class follow_AI : MonoBehaviour
{
    public float speed ;
    public Transform target;
	

    void FixedUpdate()
    {
		FollowStupidMethod ();		
    }
	
	void FollowStupidMethod()
	{
		Vector2 toTarget = target.transform.position - transform.position;
		float speed = 1.5f;
		transform.Translate(toTarget * speed * Time.deltaTime);

//		transform.Rotate(new Vector3(0, 90, 0), Space.Self); 
//		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

//		transform.LookAt(target.position);
//		transform.Rotate(new Vector3(0, 90, 0), Space.Self); 
//		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
//		transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
	}

}

