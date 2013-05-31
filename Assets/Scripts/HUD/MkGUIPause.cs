using UnityEngine;
using System.Collections;

public class MkGUIPause : MonoBehaviour {

    //public GameObject activate;
    public GameObject btnPause;
    public bool pauseIsClicked = false;

	// Use this for initialization
	void Awake () 
    {
      //  activateDialog = GameObject.FindGameObjectWithTag("activateDialog");
	}
	
	// Update is called once per frame
	void Update () {
        menuPause();
	}

    void OnMouseDown()
    {
        if (MKGUIPauseDialog.instance.activate == false)
        {
            MKGUIPauseDialog.instance.activate = true;
            MKGUIPauseDialog.instance.menuD = MKGUIPauseDialog.Menu.InGame;
           // activateDialog.myGUI();
            pauseIsClicked = true;
        }
        else
        {
            MKGUIPauseDialog.instance.activate = false;
            MKGUIPauseDialog.instance.menuD = MKGUIPauseDialog.Menu.Pause;
            Debug.Log(MkTextProvider.getTextFromId("yes"));
            pauseIsClicked = false;
        }
    }

    void menuPause()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            if (MKGUIPauseDialog.instance.activate == false)
            {
                MKGUIPauseDialog.instance.activate = true;
                MKGUIPauseDialog.instance.menuD = MKGUIPauseDialog.Menu.Pause;
            }
            else
            {
                if (MKGUIPauseDialog.instance.activate == true)
                    MKGUIPauseDialog.instance.activate = false;
            }
    }
}
