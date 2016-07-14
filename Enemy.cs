using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	#region Utilities
	float pesoQueda;
	public bool follow;
	public Transform shootPosition;
    Soul soul;
	public GameObject enemyRef;
	public GameObject playerRef;
	Animator anim;
	public GameObject plataform;
	public bool facingRight = true;
	SpriteRenderer spriteRend;
	public bool Kamizazi;
	public Collider2D colRef;
	#endregion
	

	void Start()
	{
		colRef = GetComponent<Collider2D> ();
		spriteRend = GetComponent<SpriteRenderer> ();
		pesoQueda = Random.Range (10, 50);
		anim = GetComponent<Animator> ();
		soul = GetComponent<Soul> ();
		enemyRef = this.gameObject;
		playerRef = GameObject.FindGameObjectWithTag ("Player");
		if (soul.canShot) {
			StartCoroutine("ActivateBullets");
		}
	}

	void Update(){
		SelfDestruct ();
		Kamizaki ();
	}

//	void OnBecameInvisible() {
//		print ("MORREU!!!!");
//	}

	public void Kamizaki(){
		if(Kamizazi)
		{
			anim.SetTrigger ("Jump");
			transform.Translate (0, -pesoQueda*Time.deltaTime, 0);
		}

		if (playerRef != null && gameObject.tag == "Enemy") {
			float distance = Vector3.Distance (enemyRef.transform.localPosition, playerRef.transform.localPosition);
			if (distance < 10f) {
				if (!follow) {
					Kamizazi = true;
					colRef.isTrigger = true;
				} else {
					colRef.isTrigger = false;
				}	
			}
		}
	}


	public void SelfDestruct(){
		if (transform.position.y <= -20f || transform.position.y >= 30f) {
			Destroy (this.gameObject);
		}
	}

	public void AddPoints(int points) {
		SumScore.Add(points);
	}

//	public void CheckHighScore () {
//		if (SumScore.Score > SumScore.HighScore)
//			SumScore.SaveHighScore();
//	}

//	void FollowStupid()
//	{
//		Vector2 toTarget = playerRef.transform.position	;
//		float speed = 1.5f;
//		transform.Translate(toTarget * speed * Time.deltaTime);
//	}


	//Shoot business
    IEnumerator ActivateBullets ()
    {
        soul = GetComponent<Soul>();
        soul.Move(transform.up * -1);

        if (soul.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            for (int i = 0; i < transform.childCount; i++ )
            {
				soul.Shot (shootPosition);
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(soul.shotDelay);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Boost")
        {
            //			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (500, 500));
            //transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
            AddPoints(1);
            soul.Explosion();
            Destroy(gameObject);
        }

        if (c.tag == "Rainbow")
        {
            GetComponent<AudioSource>().Play();
            soul.Burn(spriteRend);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
        }

        if (c.tag == "MaxBoost")
        {
            AddPoints(1);
            soul.Explosion();
            Destroy(gameObject);
        }

        if (c.tag == "KillZone")
        {
            Destroy(gameObject);
        }
    }
 }

