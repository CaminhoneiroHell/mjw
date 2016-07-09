using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    public GameObject[] wave;

    
    void Start()
	{
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }


    void Spawn()
    {
	    int enemyIndex = Random.Range(0, wave.Length);
	    Instantiate(wave[enemyIndex], transform.position, transform.rotation);
    }
}
