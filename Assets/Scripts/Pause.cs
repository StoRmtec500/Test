using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public PauseMenue pauseactiv;

    public void Awake()
    {
        pauseactiv = gameObject.GetComponent<PauseMenue>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (pauseactiv.enabled == true && Input.GetKey("escape"))
        {
            //HideCourser hide = gameObject.GetComponent<HideCourser>();
            //hide.enabled = true;

            Pause pause = gameObject.GetComponent<Pause>();
            pause.enabled = false;

            pauseactiv.enabled = true;

            Time.timeScale = 1f;
        }
	}

    public void theFirstMenu()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        GUI.Box(new Rect(0, 0, 200, 200), "Test");
        GUI.EndGroup();
    }

    void OnGUI()
    {
        theFirstMenu();
    }
}
