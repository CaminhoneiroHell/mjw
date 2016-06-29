

using UnityEngine;
using System.Collections;

public class Spectro : MonoBehaviour
{
    /* Soul Component
    Soul soul;
     
    // Score Points
    public int points = 100;
     
    IEnumerator Begin()
    {
        // Get the Soul component
        soul = GetComponent<Soul>();
     
        // Move in the negative direction of the local Y-axis
        soul.Move(transform.right * -1);
        // if can shot is false, end coroutine here.
        if (soul.canShot == false)
        {
            yield break;
        }
     
        while (true)
        {
            // Get all child elements
            for (int i = 0; i < transform.childCount; i++)
            {
     
                Transform shotPosition = transform.GetChild(i);
     
                //Make the bullet, using the gameobject position / rotation
                soul.Shot(shotPosition);
     
                // Play the shot sound effect
                GetComponent<AudioSource>().Play();
     
            }
            yield return new WaitForSeconds(soul.shotDelay);
        }
    }
     */
    void Awake()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Find("Music").GetComponent<AudioSource>().Pause();
        StartCoroutine("MusicTemp");
        StartCoroutine("MovementTemp");
    }
    void OnTriggerEnter2D(Collider2D c)
    {



        // Get the layer name
//        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // Return immediately if the layer not "Boost (Player)"
//        if (layerName == "Boost (Player)" || layerName == "Player")
//        {
//            Application.LoadLevel(5);
//        }

    }
    IEnumerator MusicTemp()
    {
        yield return new WaitForSeconds(17.5f);//Só refazer o tempo na versão final
        GameObject.Find("Music").GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Stop();
        GameObject.Destroy(gameObject);
    }
    IEnumerator MovementTemp()
    {
        yield return new WaitForSeconds(15f);
        GetComponent<Rigidbody2D>().AddForce(Vector3.left*1000f);

    }
}

