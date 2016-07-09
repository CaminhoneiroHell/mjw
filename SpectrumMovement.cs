using UnityEngine;
using System.Collections;

public class SpectrumMovement : MonoBehaviour {
	Vector2 vel = new Vector2 (1 * 30,1*30); // his velocity
	public bool canmove = true;
	bool canroutine = true;
	public int Life = 100;
    Soul soul;
    public int points = 100;

    void Update()
    {
        if (canroutine)
            StartCoroutine("SpectrumPhases");

        if (canmove)
        { // if spectrum isnt in the '' stopped phase, he can have a velocity
            GetComponent<Rigidbody2D>().velocity = vel; // keep his velocity with the VEL values
            GetComponent<Animator>().SetBool("flying", false);
            gameObject.layer = LayerMask.NameToLayer("Meteor");
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("flying", true);
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }

    }

	void OnTriggerEnter2D(Collider2D c)
	{
        soul = GetComponent<Soul>();
//        player = GetComponent<Player>();
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

		if (c.tag == "SpectrumRectsH") 
		{
			vel.x = vel.x *-1; 
		}
		if (c.tag == "SpectrumRectsV") 
		{
			vel.y= vel.y *-1;
		}
        if (layerName == "Boost (Player)" && canmove == false)
        {
            Life = Life - 1;
            soul.Explosion();
                if(Life <= 0 )
                {
                    PlayerPrefs.SetFloat("Ascore", (PlayerPrefs.GetFloat("Ascore") + 999f));
                        soul.Explosion ();

                        Destroy (gameObject);

//                        Application.LoadLevel(1);
                }
        }
        else if (layerName == "Player")
        {

            GameObject.Destroy(c.gameObject);
            soul.Explosion();
        }
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