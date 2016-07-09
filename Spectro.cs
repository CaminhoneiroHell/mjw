

using UnityEngine;
using System.Collections;

public class Spectro : MonoBehaviour
{
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

