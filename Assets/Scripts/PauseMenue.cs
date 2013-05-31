using UnityEngine;
using System.Collections;

public class PauseMenue : MonoBehaviour {

    public bool pauseactiv = false;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            Time.timeScale = 0f;

            //EyeMovement script1 = gameObject.GetComponent<EyeMovement>();
            //script1.enabled = false;

            Pause script2 = gameObject.GetComponent<Pause>();
            script2.enabled = true;

            //HideCourser script3 = gameObject.GetComponent<HideCourser>();
            //script3.enabled = false;

            Screen.showCursor = true;
            pauseactiv = true;
        }
	}
}
