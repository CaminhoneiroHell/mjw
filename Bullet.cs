using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 20;
    public float lifeTime = 5;
    public int power = 1;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
        Destroy(gameObject, lifeTime);
    }
}
