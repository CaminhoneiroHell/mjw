using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 20;
    public float lifeTime = 5;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
        Destroy(gameObject, lifeTime);
    }

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Boost" || c.tag == "MaxBoost")
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (300, 1000));
	}
}
