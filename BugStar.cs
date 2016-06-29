using UnityEngine;
using System.Collections;

public class BugStar : MonoBehaviour
{
    public IEnumerator change()
    {
        yield return new WaitForSeconds(0);
//        Application.LoadLevel(4);
    }
}