using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Soul : MonoBehaviour
{
    public float speed;
    public float shotDelay;
    public GameObject bullet;
    public bool canShot;
	public GameObject effect;
	public GameObject explosion;
	public GameObject burning;
	public GameObject bornEffect;

	public void Born ()
	{
		Instantiate (bornEffect, transform.position, transform.rotation);        
	}

    public void Explosion ()
    {
        Instantiate (explosion, transform.position, transform.rotation);        
    }

	public void EspecialEffect ()
	{
		Instantiate (effect, transform.position, transform.rotation);        
	}

    public void Shot(Transform origin)
    {
        Instantiate(bullet, origin.position, origin.rotation);
    }

    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

	public void Burn(SpriteRenderer spriteRend)
	{
		spriteRend.color = new Vector4 (0, 0, 0, 1);	
		burning.SetActive(true);
	}
}
