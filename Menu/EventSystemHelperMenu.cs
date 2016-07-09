using UnityEngine;
using System.Collections;

public class EventSystemHelperMenu : MonoBehaviour
{
    void Update()
    {
		if (Input.anyKey)
		{
			HelperHelper(true);
			Destroy (this);
		}
		else
		{
			HelperHelper(false);
		}
    }

	public void HelperHelper(bool Helperbool)
	{
		FindObjectOfType<FrontMenu>().beginGameHelper(Helperbool);
	}

}