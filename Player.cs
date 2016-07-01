using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	#region Variables
    public bool facingRight = true;
    public static bool boostActive;
    Soul soul;
    Animator animator;
    public bool flying;
	public float speedIncreace = 0.1f;
	public float speed = 0.1f;
	public float boost = 10000;

	public GameObject spawnerFollow;	
	public GameObject jetBoost;
	public GameObject jetLaser;
	public GameObject bgBack;
	public GameObject bgMidle;
	public GameObject bgFront;
	public GameObject warning;
	public GameObject sparckles;
	public AudioSource slowMusic;
	public AudioSource hardMusic;
	
	public Texture2D[] textureList;
	private int index;
	Texture2D atualTexture;
	bool change;
	SpriteRenderer spriteRend;
	bool activeHyperBoost;
	private Vector3 mousePosition;

	bool rankCheckerD;
	bool rankCheckerC;
	bool rankCheckerB;
	bool rankCheckerA;
	bool rankCheckerS;
	bool rankCheckerSS;

	#endregion

	public void ChangeTexture()
	{
		index = Random.Range (0, textureList.Length);
		atualTexture = textureList [index];
		bgBack.GetComponent<Renderer>().material.mainTexture = 
		GetComponent<Renderer> ().material.mainTexture = atualTexture;
	}

	public enum Rank
	{
		SS,
		S,
		A,
		B,
		C,
		D,
		E
	}

	public Rank rank;

	void Start()
	{	
		soul = GetComponent<Soul>();
		soul.Born ();
		animator = GetComponent<Animator>();
		spriteRend = GetComponent<SpriteRenderer> ();
		slowMusic.GetComponent<AudioSource> ();
		hardMusic.GetComponent<AudioSource> ();
		AtualizaRank ();
  	}

	public void AtualizaRank()
	{
		if (SumScore.Score > 1)
			rank = Rank.D;
		if (SumScore.Score > 3)
			rank = Rank.C;
		if (SumScore.Score > 5)
			rank = Rank.B;
		if (SumScore.Score > 6)
			rank = Rank.A;
		if (SumScore.Score > 7)
			rank = Rank.S;
		if (SumScore.Score > 8)
			rank = Rank.SS;
		
		switch(rank)
		{
		case Rank.SS:
			rankCheckerSS = true;
			Debug.Log("200% PUTAÇO");
			break;
		case Rank.S:
			rankCheckerS = true;
			break;
		case Rank.A:
			rankCheckerA = true;
			break;
		case Rank.B:
			rankCheckerB = true;
			break;
		case Rank.C:
			rankCheckerC = true;
			break;
		case Rank.D:
			rankCheckerD = true;
			break;
		case Rank.E:
			Debug.Log ("Lixo");
			break;
		}
	}

	void Boost()
	{
		Camera.main.GetComponent<Camera2DFollow>().PlayShake();
		this.gameObject.tag = "Boost";
		jetLaser.SetActive (true);
		transform.rotation = Quaternion.identity;
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, boost));
		if(!activeHyperBoost)
		boostActive = true;

		if(rankCheckerD)
			sparckles.SetActive (true);
		if(rankCheckerC)
			ChangeTexture ();
		if (rankCheckerB)
			spawnerFollow.SetActive (true);
	}

	void Fly()
	{
		boostActive = false;
		jetLaser.SetActive (false);
		this.gameObject.tag = "Player";
		atualTexture = textureList [0];
		bgBack.GetComponent<Renderer> ().material.mainTexture = GetComponent<Renderer> ().material.mainTexture = atualTexture;
	}

	public IEnumerator HyperBoostFinisher()
	{
		yield return new WaitForSeconds(30f);
		activeHyperBoost = false;
		animator.SetTrigger ("break");
		bgBack.transform.localScale += new Vector3(0,-5000,1);
	}

    void FixedUpdate()
	{
		#region Inputs
		if (Input.GetMouseButton(0) && activeHyperBoost == false) {
			Boost ();
		} else {
			Fly ();
		}
		#endregion

		BgMove ();
		PlayerMover ();
    }

	public void PlayerMover()
	{
		#region Comandos movimento
		float x = Input.GetAxisRaw("Horizontal");
		Vector2 direction = new Vector2(x, 0).normalized;

		if (x > 0 && !facingRight)
			Flip();
		else if (x < 0 && facingRight)
			Flip();

		soul.Move(direction);

		if (boostActive && !activeHyperBoost)
		{
			hardMusic.volume += 0.5f * Time.deltaTime;
			slowMusic.volume -= 0.5f * Time.deltaTime;
			animator.SetBool("flying", true);
			speed++;
			if (speed >= 0.5f)
				speed = 0.5f;
		}
		else if(!boostActive && !activeHyperBoost)
		{
			hardMusic.volume -= 0.5f * Time.deltaTime;
			slowMusic.volume += 0.5f * Time.deltaTime;
			animator.SetBool("flying", false);
			speed--;
			if (speed <= 0.1f)
				speed = 0.1f;
			this.gameObject.tag = "Player";
			jetBoost.SetActive (false);
		}else if (activeHyperBoost && !boostActive){ 
			hardMusic.volume += 0.5f * Time.deltaTime;
			slowMusic.volume -= 0.5f * Time.deltaTime;
			this.gameObject.tag = "MaxBoost";
			jetBoost.SetActive (true);
		}if(!activeHyperBoost)
			Time.timeScale = 1;
		#endregion

		#region Limita x e y
		if (transform.position.x <= -18f)
			transform.position = new Vector3(-18f, transform.position.y, transform.position.z);
		else if (transform.position.x >= 18f)
			transform.position = new Vector3(18f, transform.position.y, transform.position.z);

		if (transform.position.y <= -7f)
			transform.position = new Vector3(transform.position.x, -7f, transform.position.z);
		else if (transform.position.y >= -5f)
			transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
		#endregion
	}

	public IEnumerator HyperBoost()
	{
		yield return new WaitForSeconds(6.2f); //Tempo Certo
//		yield return new WaitForSeconds(1f); //Tempo Lixo
		animator.SetTrigger ("charge");
		transform.Translate (0, 10, 0);
		bgFront.SetActive (false);
		bgMidle.SetActive (false);
		activeHyperBoost = true;
		bgBack.transform.localScale += new Vector3(0,5000,1);
		warning.SetActive (false);
		Time.timeScale = 2.5f;
		StartCoroutine ("HyperBoostFinisher");
    }

	public void BgMove()
	{
		float backgroundY = Mathf.Repeat(Time.time * speed, 1);
		Vector2 bunnyOffset = new Vector2(0, backgroundY);

		float backgroundY2 = Mathf.Repeat(Time.time * 0.1f, 1);
		Vector2 bunnyOffset2 = new Vector2(0, backgroundY2);
		bgBack.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", bunnyOffset);
		bgFront.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", bunnyOffset2);
		bgMidle.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", bunnyOffset2);
	}

    void Flip()
    {
        facingRight = !facingRight;
		Vector3 theScale = spriteRend.transform.localScale;
        theScale.x *= -1;
        spriteRend.transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D c)
	{
		AtualizaRank ();

		if (c.tag == "KillZone") {
			soul.EspecialEffect ();
			soul.Explosion ();
			Destroy (gameObject);
		}

		if(boostActive == false && this.gameObject.tag == "Player")
		{
			if (c.tag == "Enemy") {
				soul.EspecialEffect ();
				soul.Explosion ();
				Destroy (gameObject);
			}

			else if (c.tag == "EnemyFall") {
				soul.EspecialEffect ();
				soul.Explosion ();
				Destroy (gameObject);
			}
        }

		if (c.tag == "Carrot" && !activeHyperBoost) {
			warning.SetActive (true);
			StartCoroutine ("HyperBoost");
			Destroy (c.gameObject);
		}
  	}
}