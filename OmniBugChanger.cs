using UnityEngine;
using System.Collections;

public class OmniBugChanger : MonoBehaviour
{
	
    public IEnumerator WarningBug(){
		yield return new WaitForSeconds (0);
	}
}