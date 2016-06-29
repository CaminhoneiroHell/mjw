using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
//    [HideInInspector]
    public bool facingRight = true;
    public static bool boostActive;
    Soul soul;
    Animator animator;
    public bool flying;
	public float speedIncreace = 0.1f;
	public float speed = 0.1f;
	public float boost = 10000;
	
	public GameObject jetBoost;
	public GameObject jetLaser;
	public GameObject bgBack;
	public GameObject bgMidle;
	public GameObject bgFront;
	public GameObject warning;
	public AudioSource slowMusic;
	public AudioSource hardMusic;
	
	public Texture2D[] textureList;
	private int index;
	Texture2D atualTexture;
	bool change;
	SpriteRenderer spriteRend;
//	public Collider2D colRef;
	bool chargeJump;
	private Vector3 mousePosition;

	public void ChangeTexture()
	{
		index = Random.Range (0, textureList.Length);
		atualTexture = textureList [index];
		bgBack.GetComponent<Renderer>().material.mainTexture = GetComponent<Renderer> ().material.mainTexture = atualTexture;
	}

	void Start()
	{	
		soul = GetComponent<Soul>();
		soul.Born ();
		animator = GetComponent<Animator>();
		spriteRend = GetComponent<SpriteRenderer> ();
		slowMusic.GetComponent<AudioSource> ();
		hardMusic.GetComponent<AudioSource> ();
	}

//	float nextTime = 0;
//	void Update()
//	{
//			if (Time.fixedTime > nextTime) {
//					nextTime = Time.fixedTime + 5;
//					print ("Time Up");
//			}
//	}

//	void Update()
//	{
//		if (SumScore.Score > 3) {
//			print ("D");
//		}
//
//		if (SumScore.Score > 4) {
//			print ("C");
//		}
//		if (SumScore.Score > 5) {
//			print ("B");
//		}
//		if (SumScore.Score > 6) {
//			print ("A");
//		}
//		if (SumScore.Score > 7) {
//			print ("S");
//		}
//	}

    void FixedUpdate()
	{
		BgMove ();
		Debug.Log (boostActive);
//		bgBack.transform.rotation = this.gameObject.transform.rotation;
//		this.gameObject.transform.rotation = bgBack.gameObject.transform.rotation;

		if (Input.GetMouseButton(0)) {
			Camera.main.GetComponent<Camera2DFollow>().PlayShake();
			this.gameObject.tag = "Boost";
			jetLaser.SetActive (true);
			transform.rotation = Quaternion.identity;
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, boost));
			ChangeTexture ();
			if(!chargeJump)
				boostActive = true;
		} else {
			if (Input.GetMouseButtonUp (0)) {
				boostActive = false;
				jetLaser.SetActive (false);
				this.gameObject.tag = "Player";
			}
		}
		
		if (Input.GetMouseButtonDown (1) && chargeJump == false) {
			StartCoroutine ("HyperBoost");
			warning.SetActive (true);
		}

		if (Input.GetKeyDown(KeyCode.X)) {
			normalSpace ();
			chargeJump = false;
			animator.SetTrigger ("break");
		}

		#region Comandos movimento
		float x = Input.GetAxisRaw("Horizontal");
		Vector2 direction = new Vector2(x, 0).normalized;

        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();

        soul.Move(direction);

		if (boostActive && !chargeJump)
		{
//          GetComponent<AudioSource>().Play();
			hardMusic.volume += 0.5f * Time.deltaTime;
			slowMusic.volume -= 0.5f * Time.deltaTime;
            animator.SetBool("flying", true);
				speed++;
				if (speed >= 0.5f)
					speed = 0.5f;
        }
		else if(!boostActive && !chargeJump)
		{
			hardMusic.volume -= 0.5f * Time.deltaTime;
			slowMusic.volume += 0.5f * Time.deltaTime;
            animator.SetBool("flying", false);
				speed--;
			if (speed <= 0.1f)
				speed = 0.1f;
			this.gameObject.tag = "Player";
			jetBoost.SetActive (false);
		}else if (chargeJump && !boostActive){ 
			hardMusic.volume += 0.5f * Time.deltaTime;
			slowMusic.volume -= 0.5f * Time.deltaTime;
			this.gameObject.tag = "MaxBoost";
			jetBoost.SetActive (true);
		}
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

		//End Update
    }

	public IEnumerator HyperBoost()
	{
//		yield return new WaitForSeconds(6.5f); //Tempo Certo
		yield return new WaitForSeconds(1f); //Tempo Lixo
		animator.SetTrigger ("charge");
		transform.Translate (0, 10, 0);
		bgFront.SetActive (false);
		bgMidle.SetActive (false);
		chargeJump = true;
		HyperSpace ();
		warning.SetActive (false);
    }

//	public void NormalizeRotation()
//	{
//		Vector3 temp = transform.rotation.eulerAngles;
//		temp.z = 0f;
//		transform.rotation = Quaternion.Euler (temp);
//	}

	public void HyperSpace()
	{
		bgBack.transform.localScale += new Vector3(0,5000,1);
	}

	public void normalSpace()
	{
		bgBack.transform.localScale += new Vector3(0,-5000,1);
//		colRef.isTrigger = false;
	}

	public void BgMove()
	{
		float backgroundY = Mathf.Repeat(Time.time * speed, 1);
		Vector2 bunnyOffset = new Vector2(0, backgroundY);
		bgBack.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", bunnyOffset);
		bgFront.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", bunnyOffset);
		bgMidle.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", bunnyOffset);
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
		soul.EspecialEffect ();

		if (c.tag == "KillZone") {
			soul.Explosion ();
			Destroy (gameObject);
		}

		if(boostActive == false && this.gameObject.tag == "Player")
		{
			soul.EspecialEffect ();
			if (c.tag == "Enemy") {
				soul.Explosion ();
				Destroy (gameObject);
			}

			else if (c.tag == "EnemyFall") {
				soul.Explosion ();
				Destroy (gameObject);
			}
        }
	}

}