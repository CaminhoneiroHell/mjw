using UnityEngine;
using System.Collections;

public class SpectroBoss : MonoBehaviour {
	bool canmove = true;
	bool canroutine = true;
	Soul soul;
	public GameObject playeRef;
	public int Life = 10;
	public GameObject darkRainbow;
	Vector2 vel = new Vector2 (1*30,1*30);

	void Start()
	{
		playeRef = GameObject.FindGameObjectWithTag ("Player");
	}

    void Update()
    {
		SpectroController ();
    }

	void SpectroController()
	{
		if (canroutine)
			StartCoroutine("SpectrumPhases");

		if (canmove)
		{ 
			GetComponent<Rigidbody2D>().velocity = vel;
			GetComponent<Animator>().SetBool("flying", true);
			darkRainbow.SetActive (false);
			GetComponent<AudioSource> ().volume += 1;
			gameObject.tag = "KillZone";
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			GetComponent<Animator>().SetBool("flying", false);
			darkRainbow.SetActive (true);
			GetComponent<AudioSource> ().volume -= 1;
			gameObject.tag = "Enemy";
		}
	}

	public void AddPoints(int points) {
		SumScore.Add(points);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        soul = GetComponent<Soul>();

		if (c.tag == "SpectrumRectsH") 
		{
			vel.x = vel.x *-1; 
		}
		if (c.tag == "SpectrumRectsV") 
		{
			vel.y= vel.y *-1;
		}
		if (c.tag == "Boost" && canmove == false)
        {
            Life = Life - 1;
            soul.Explosion();
                if(Life <= 0 )
                {
					AddPoints (1);
					soul.Explosion ();
				StartCoroutine ("SpecDie");
				playeRef.GetComponent<Player> ().SpectroBattleFinisher ();
					this.gameObject.SetActive (false);
                }
        }
		else if (c.tag == "Player")
        {
            GameObject.Destroy(c.gameObject);
            soul.Explosion();
        }
	}

	IEnumerator SpecDie()
	{
		yield return new WaitForSeconds (3f);
	}

	IEnumerator SpectrumPhases()
	{
		canroutine = false;
		canmove = true;
		yield return new WaitForSeconds (10f);
		canmove = false;
		yield return new WaitForSeconds (3f);
		canmove = true;
		canroutine = true;
		StopCoroutine ("SpectrumPhases");	
	}
}