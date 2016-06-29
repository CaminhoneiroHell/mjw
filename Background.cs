using UnityEngine;
using System.Collections;
public class Background : MonoBehaviour
{
    void FixedUpdate()
    {
		float y = Mathf.Repeat(Time.time * 0.01f, 1);
        Vector2 offset = new Vector2(0, y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}