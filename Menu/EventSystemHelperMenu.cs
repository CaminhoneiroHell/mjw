using UnityEngine;
using System.Collections;

public class EventSystemHelperMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HelperHelper(true);
        }
        else
        {
            HelperHelper(false);
        }
    }

    public void HelperHelper(bool Helperbool)
    {
        FindObjectOfType<Bunny>().beginGameHelper(Helperbool);
    }

}

/*
using UnityEngine;
using System.Collections;

public class EventSystemHelper : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void HelperHelper(bool Helperbool){
		FindObjectOfType<Player> ().BoostHelper (Helperbool);
	}
}
*/