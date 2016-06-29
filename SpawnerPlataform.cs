using UnityEngine;
using System.Collections;

public class SpawnerPlataform: MonoBehaviour
{
    // The amount of time between each spawn.
    public float spawnTime = 5f;

    // The amount of time before spawning starts.
    public float spawnDelay = 3f;

    // Array of enemy prefabs.
    public GameObject[] wave;

    
    void Start()
    {
            // Start calling the Spawn function repeatedly after a delay .
            InvokeRepeating("Spawn", spawnDelay, spawnTime);
        
    }


    void Spawn()
    {
        
        
            // Instantiate a random enemy.
            int enemyIndex = Random.Range(0, wave.Length);
            Instantiate(wave[enemyIndex], transform.position, transform.rotation);

            //Play the spawning effect from all of the particle systems.
            foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
            {
                p.Play();
            }
        
    }
}
