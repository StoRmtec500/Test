using UnityEngine;
using System.Collections;

public class MkScreen : MonoBehaviour {

    private bool show;
    protected int contentIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showScreen()
    {
        this.show = true;
    }

    public void hideScreen()
    {
        this.show = false;
    }
}
